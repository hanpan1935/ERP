using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.WarehouseManagement.OtherOuts.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Entities;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;
using Volo.Abp.Domain.Repositories;
using Lanpuda.ERP.Utils.UniqueCode;
using Volo.Abp;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts;


[Authorize]
public class OtherOutAppService : ERPAppService, IOtherOutAppService
{
    private readonly IOtherOutRepository _otherOutRepository;
    private readonly IOtherOutDetailRepository _otherOutDetailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;

    public OtherOutAppService(
        IOtherOutRepository repository,
        IOtherOutDetailRepository otherOutDetailRepository,
        IUniqueCodeUtils uniqueCodeUtils,
        IInventoryRepository inventoryRepository,
        IInventoryLogRepository inventoryLogRepository
        )
    {
        _otherOutRepository = repository;
        _otherOutDetailRepository = otherOutDetailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
    }


    [Authorize(ERPPermissions.OtherOut.Create)]
    public async Task CreateAsync(OtherOutCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.OtherOutPrefix); ;
        OtherOut otherOut = new OtherOut(id);
        otherOut.Number = number;
        otherOut.Description = input.Description;
        List<OtherOutDetail> details = new List<OtherOutDetail>();
        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            OtherOutDetail detail = new OtherOutDetail(detailId);
            detail.OtherOutId = id;
            detail.Sort = i;
            detail.ProductId = item.ProductId;
            detail.LocationId = item.LocationId;
            detail.Batch = item.Batch;
            detail.Quantity = item.Quantity;
            details.Add(detail);
        }

        //CheckIsProductRepeat(details);

        OtherOut result = await _otherOutRepository.InsertAsync(otherOut);
        await _otherOutDetailRepository.InsertManyAsync(details);
    }



    [Authorize(ERPPermissions.OtherOut.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        OtherOut otherOut = await _otherOutRepository.FindAsync(id);
        if (otherOut == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        if (otherOut.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库,无法删除");
        }

        await _otherOutRepository.DeleteAsync(otherOut);
    }



    [Authorize(ERPPermissions.OtherOut.Update)]
    public async Task<OtherOutDto> GetAsync(Guid id)
    {
        var entity = await _otherOutRepository.GetAsync(id, true);
        var dto = ObjectMapper.Map<OtherOut, OtherOutDto>(entity);
        return dto;
    }



    [Authorize(ERPPermissions.OtherOut.Default)]
    public async Task<PagedResultDto<OtherOutDto>> GetPagedListAsync(OtherOutPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _otherOutRepository.WithDetailsAsync();
        query = query
             .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
             .WhereIf(input.IsSuccessful != null, m => m.IsSuccessful.Equals(input.IsSuccessful))
             ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<OtherOutDto>(totalCount, ObjectMapper.Map<List<OtherOut>, List<OtherOutDto>>(result));
    }



    [Authorize(ERPPermissions.OtherOut.Out)]
    public async Task OutedAsync(Guid id)
    {
        OtherOut otherOut = await _otherOutRepository.GetAsync(id, true);

        if (otherOut == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (otherOut.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库");
        }


        if (otherOut.Details == null || otherOut.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }

        foreach (var item in otherOut.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }

        
        otherOut.IsSuccessful = true;
        otherOut.SuccessfulTime = Clock.Now;
        otherOut.KeeperUserId = CurrentUser.Id;

        foreach (var item in otherOut.Details)
        {
            InventoryLog inventoryLog = new InventoryLog(GuidGenerator.Create());
            inventoryLog.Number = otherOut.Number;
            inventoryLog.ProductId = item.ProductId;
            inventoryLog.LocationId = item.LocationId;
            inventoryLog.LogTime = Clock.Now;
            inventoryLog.LogType = InventoryLogType.OtherOut;
            inventoryLog.Batch = item.Batch;
            inventoryLog.OutQuantity = item.Quantity;
            double afterQuantity = await _inventoryRepository.OutAsync(item.LocationId, item.ProductId, item.Quantity, item.Batch);
            inventoryLog.AfterQuantity = afterQuantity;
            await _inventoryLogRepository.InsertAsync(inventoryLog);
        }
        var dto = ObjectMapper.Map<OtherOut, OtherOutDto>(otherOut);
    }



    [Authorize(ERPPermissions.OtherOut.Update)]
    public async Task UpdateAsync(Guid id, OtherOutUpdateDto input)
    {
        OtherOut otherOut = await _otherOutRepository.FindAsync(id);
        if (otherOut == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //

        if (otherOut.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库！无法编辑");
        }

        otherOut.Description = input.Description;

        List<OtherOutDetail> createList = new List<OtherOutDetail>();
        List<OtherOutDetail> updateList = new List<OtherOutDetail>();
        List<OtherOutDetail> deleteList = new List<OtherOutDetail>();
        List<OtherOutDetail> dbList = await _otherOutDetailRepository.GetListAsync(m => m.OtherOutId == id);

        foreach (var item in input.Details)
        {
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                OtherOutDetail detail = new OtherOutDetail(detailId);
                detail.OtherOutId = id;
                detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.Batch = item.Batch;
                detail.Quantity = item.Quantity;
                createList.Add(detail);
            }
            else //编辑
            {
                OtherOutDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.Batch = item.Batch;
                detail.Quantity = item.Quantity;
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
        var result = await _otherOutRepository.UpdateAsync(otherOut);
        await _otherOutDetailRepository.InsertManyAsync(createList);
        await _otherOutDetailRepository.UpdateManyAsync(updateList);
        await _otherOutDetailRepository.DeleteManyAsync(deleteList);
    }
}
