using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.Utils.UniqueCode;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.InventoryMoves.Dtos;
using Lanpuda.ERP.WarehouseManagement.InventoryMoves;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves;

[Authorize]
public class InventoryMoveAppService : ERPAppService, IInventoryMoveAppService
{
    private readonly IInventoryMoveRepository _inventoryMoveRepository;
    private readonly IInventoryMoveDetailRepository _inventoryMoveDetailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;

    public InventoryMoveAppService(
        IInventoryMoveRepository repository,
        IInventoryMoveDetailRepository inventoryMoveDetailRepository,
        IUniqueCodeUtils uniqueCodeUtils,
         IInventoryRepository inventoryRepository,
        IInventoryLogRepository inventoryLogRepository
        )
    {
        _inventoryMoveRepository = repository;
        _inventoryMoveDetailRepository = inventoryMoveDetailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
    }


    [Authorize(ERPPermissions.InventoryMove.Create)]
    public async Task CreateAsync(InventoryMoveCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.InventoryMovePrefix); ;
        InventoryMove inventoryMove = new InventoryMove(id);
        inventoryMove.Number = number;
        inventoryMove.Reason = input.Reason;
        inventoryMove.Remark = input.Remark;

        List<InventoryMoveDetail> details = new List<InventoryMoveDetail>();
        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            InventoryMoveDetail detail = new InventoryMoveDetail(detailId);
            detail.InventoryMoveId = id;
            detail.Sort = i;
            detail.ProductId = item.ProductId;
            detail.OutLocationId = item.OutLocationId;
            detail.Batch = item.Batch;
            detail.Quantity = item.Quantity;
            detail.InLocationId = item.InLocationId;
            details.Add(detail);
        }

        InventoryMove result = await _inventoryMoveRepository.InsertAsync(inventoryMove);
        await _inventoryMoveDetailRepository.InsertManyAsync(details);
    }


    [Authorize(ERPPermissions.InventoryMove.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        InventoryMove inventoryMove = await _inventoryMoveRepository.FindAsync(id);
        if (inventoryMove == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        if (inventoryMove.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经移库,无法删除");
        }

        await _inventoryMoveRepository.DeleteAsync(inventoryMove);
    }



    [Authorize(ERPPermissions.InventoryMove.Update)]
    public async Task<InventoryMoveDto> GetAsync(Guid id)
    {
        var entity = await _inventoryMoveRepository.GetAsync(id, true);
        var dto = ObjectMapper.Map<InventoryMove, InventoryMoveDto>(entity);
        return dto;
    }



    [Authorize(ERPPermissions.InventoryMove.Default)]
    public async Task<PagedResultDto<InventoryMoveDto>> GetPagedListAsync(InventoryMovePagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _inventoryMoveRepository.WithDetailsAsync();
        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(input.KeeperUserId != null, x => x.KeeperUserId == input.KeeperUserId)
            .WhereIf(!input.Reason.IsNullOrWhiteSpace(), x => x.Reason.Contains(input.Reason))
            .WhereIf(!input.Remark.IsNullOrWhiteSpace(), x => x.Remark.Contains(input.Remark))
            .WhereIf(input.IsSuccessful != null, x => x.IsSuccessful == input.IsSuccessful)
            .WhereIf(input.SuccessfulTime != null, x => x.SuccessfulTime == input.SuccessfulTime)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<InventoryMoveDto>(totalCount, ObjectMapper.Map<List<InventoryMove>, List<InventoryMoveDto>>(result));
    }



    [Authorize(ERPPermissions.InventoryMove.Move)]
    public async Task MovedAsync(Guid id)
    {
        InventoryMove inventoryMove = await _inventoryMoveRepository.GetAsync(id, true);

        if (inventoryMove == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (inventoryMove.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经移库了");
        }

        //行项目部能为空
        if (inventoryMove.Details == null || inventoryMove.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }


        foreach (var item in inventoryMove.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }


       
        inventoryMove.IsSuccessful = true;
        inventoryMove.SuccessfulTime = Clock.Now;
        inventoryMove.KeeperUserId = CurrentUser.Id;

        for (int i = 0; i < inventoryMove.Details.Count; i++)
        {
            var item = inventoryMove.Details[i];
            var inventory = await _inventoryRepository.FindExistingAsync(item.ProductId, item.OutLocationId, item.Batch);
            if (inventory == null)
            {
                throw new UserFriendlyException("第" + (i+1) + "行库存不存在");
            }

            //出库
            double afterQuantity = await _inventoryRepository.OutAsync(item.OutLocationId, item.ProductId, item.Quantity, item.Batch);
            InventoryLog inventoryLogOut = new InventoryLog(GuidGenerator.Create());
            inventoryLogOut.Number = inventoryMove.Number;
            inventoryLogOut.ProductId = item.ProductId;
            inventoryLogOut.LocationId = item.OutLocationId;
            inventoryLogOut.LogTime = Clock.Now;
            inventoryLogOut.LogType = InventoryLogType.MoveOut;
            inventoryLogOut.Batch = item.Batch;
            inventoryLogOut.InQuantity = 0;
            inventoryLogOut.OutQuantity = item.Quantity;
            inventoryLogOut.AfterQuantity = afterQuantity;
            await _inventoryLogRepository.InsertAsync(inventoryLogOut);

            //入库
            var result =  await _inventoryRepository.StorageAsync(item.InLocationId,item.ProductId, item.Quantity, item.Batch);
            InventoryLog inventoryLogIn = new InventoryLog(GuidGenerator.Create());
            inventoryLogIn.Number = inventoryMove.Number;
            inventoryLogIn.ProductId = item.ProductId;
            inventoryLogIn.LocationId = item.InLocationId;
            inventoryLogIn.LogTime = Clock.Now;
            inventoryLogIn.LogType = InventoryLogType.MoveIn;
            inventoryLogIn.Batch = item.Batch;
            inventoryLogIn.InQuantity = item.Quantity;
            inventoryLogIn.OutQuantity = 0;
            inventoryLogIn.AfterQuantity = result.Quantity;
            await _inventoryLogRepository.InsertAsync(inventoryLogIn);
        }
        await _inventoryMoveRepository.UpdateAsync(inventoryMove);
        var dto = ObjectMapper.Map<InventoryMove, InventoryMoveDto>(inventoryMove);
    }



    [Authorize(ERPPermissions.InventoryMove.Update)]
    public async Task UpdateAsync(Guid id, InventoryMoveUpdateDto input)
    {
        InventoryMove inventoryMove = await _inventoryMoveRepository.FindAsync(id);
        if (inventoryMove == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        if (inventoryMove.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经调拨了，不要重复调拨");
        }

        //行项目不能为空
        if (inventoryMove.Details == null || inventoryMove.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }


        foreach (var item in inventoryMove.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }
        inventoryMove.Reason = input.Reason;
        inventoryMove.Remark = input.Remark;

        List<InventoryMoveDetail> createList = new List<InventoryMoveDetail>();
        List<InventoryMoveDetail> updateList = new List<InventoryMoveDetail>();
        List<InventoryMoveDetail> deleteList = new List<InventoryMoveDetail>();
        List<InventoryMoveDetail> dbList = await _inventoryMoveDetailRepository.GetListAsync(m => m.InventoryMoveId == id);

        foreach (var item in input.Details)
        {
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                InventoryMoveDetail detail = new InventoryMoveDetail(detailId);
                detail.InventoryMoveId = id;
                detail.ProductId = item.ProductId;
                detail.OutLocationId = item.OutLocationId;
                detail.Batch = item.Batch;
                detail.Quantity = item.Quantity;
                detail.InLocationId = item.InLocationId;
                detail.Sort = item.Sort;
                createList.Add(detail);
            }
            else //编辑
            {
                InventoryMoveDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.ProductId = item.ProductId;
                detail.OutLocationId = item.OutLocationId;
                detail.Batch = item.Batch;
                detail.Quantity = item.Quantity;
                detail.InLocationId = item.InLocationId;
                detail.Sort = item.Sort;
                updateList.Add(detail);
            }
        }
        //删除
        foreach (var dbItem in dbList)
        {
            bool isExsiting = input.Details.Any(m => m.Id == dbItem.Id);
            if (isExsiting == false)
            {
                deleteList.Add(dbItem);
            }
        }
        var result = await _inventoryMoveRepository.UpdateAsync(inventoryMove);
        await _inventoryMoveDetailRepository.InsertManyAsync(createList);
        await _inventoryMoveDetailRepository.UpdateManyAsync(updateList);
        await _inventoryMoveDetailRepository.DeleteManyAsync(deleteList);
    }
}
