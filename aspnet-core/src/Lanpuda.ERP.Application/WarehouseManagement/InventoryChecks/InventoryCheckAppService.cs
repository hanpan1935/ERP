using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.Utils.UniqueCode;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.InventoryChecks.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks;

[Authorize]
public class InventoryCheckAppService : ERPAppService, IInventoryCheckAppService
{
    private readonly IInventoryCheckRepository _inventoryCheckRepository;
    private readonly IInventoryCheckDetailRepository _inventoryCheckDetailRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;

    public InventoryCheckAppService(
        IInventoryCheckRepository repository,
        IInventoryCheckDetailRepository inventoryCheckDetailRepository,
        IUniqueCodeUtils uniqueCodeUtils,
         IInventoryRepository inventoryRepository,
        IInventoryLogRepository inventoryLogRepository
        )
    {
        _inventoryCheckRepository = repository;
        _inventoryCheckDetailRepository = inventoryCheckDetailRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
    }


    [Authorize(ERPPermissions.InventoryCheck.Create)]
    public async Task CreateAsync(InventoryCheckCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.InventoryCheckPrefix); ;
        InventoryCheck inventoryCheck = new InventoryCheck(id);
        inventoryCheck.Number = number;
        inventoryCheck.CheckDate = input.CheckDate;
        inventoryCheck.Discription = input.Discription;
        inventoryCheck.WarehouseId = input.WarehouseId;
        List<InventoryCheckDetail> details = new List<InventoryCheckDetail>();
        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            Guid detailId = GuidGenerator.Create();
            InventoryCheckDetail detail = new InventoryCheckDetail(detailId);
            detail.InventoryCheckId = id;
            detail.ProductId = item.ProductId;
            detail.LocationId = item.LocationId;
            detail.Batch = item.Batch;
            detail.InventoryQuantity = item.InventoryQuantity;
            detail.CheckType = item.CheckType;
            detail.CheckQuantity = item.CheckQuantity;

            detail.Sort = i;
            details.Add(detail);
        }

        InventoryCheck result = await _inventoryCheckRepository.InsertAsync(inventoryCheck);
        await _inventoryCheckDetailRepository.InsertManyAsync(details);
    }


    [Authorize(ERPPermissions.InventoryCheck.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        InventoryCheck inventoryCheck = await _inventoryCheckRepository.FindAsync(id);
        if (inventoryCheck == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (inventoryCheck.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经确认无法删除");
        }



        await _inventoryCheckRepository.DeleteAsync(inventoryCheck);
    }



    [Authorize(ERPPermissions.InventoryCheck.Update)]
    public async Task<InventoryCheckDto> GetAsync(Guid id)
    {
        var entity = await _inventoryCheckRepository.GetAsync(id, true);
        var dto = ObjectMapper.Map<InventoryCheck, InventoryCheckDto>(entity);
        return dto;
    }



    [Authorize(ERPPermissions.InventoryCheck.Default)]
    public async Task<PagedResultDto<InventoryCheckDto>> GetPagedListAsync(InventoryCheckPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _inventoryCheckRepository.WithDetailsAsync();
        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(input.CheckDate != null, x => x.CheckDate == input.CheckDate)
            .WhereIf(input.KeeperUserId != null, x => x.KeeperUserId == input.KeeperUserId)
            .WhereIf(input.IsSuccessful != null, x => x.IsSuccessful == input.IsSuccessful)
            .WhereIf(input.SuccessfulTime != null, x => x.SuccessfulTime == input.SuccessfulTime)
            .WhereIf(input.WarehouseId != null, x => x.WarehouseId == input.WarehouseId)
            ;

        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<InventoryCheckDto>(totalCount, ObjectMapper.Map<List<InventoryCheck>, List<InventoryCheckDto>>(result));
    }



    [Authorize(ERPPermissions.InventoryCheck.Confirm)]
    public async Task ConfirmedAsync(Guid id)
    {
        InventoryCheck inventoryCheck = await _inventoryCheckRepository.GetAsync(id, true);
        if (inventoryCheck == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (inventoryCheck.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经确认过了");
        }
        

        //行项目部能为空
        if (inventoryCheck.Details == null || inventoryCheck.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }

        inventoryCheck.IsSuccessful = true;
        inventoryCheck.SuccessfulTime = Clock.Now;
        inventoryCheck.KeeperUserId = CurrentUser.Id;

        foreach (var item in inventoryCheck.Details)
        {
            if (item.CheckType == InventoryCheckDetailType.None)
            {
                continue;
            }
            else if (item.CheckType == InventoryCheckDetailType.Add) //盘盈
            {
                if (item.CheckQuantity <= 0)
                {
                    throw new UserFriendlyException("盈亏数量必须大于0");
                }
                InventoryLog inventoryLog = new InventoryLog(GuidGenerator.Create());
                inventoryLog.Number = inventoryCheck.Number;
                inventoryLog.ProductId = item.ProductId;
                inventoryLog.LocationId = item.LocationId;
                inventoryLog.LogTime = Clock.Now;
                inventoryLog.LogType = InventoryLogType.InventoryCheckAdd;
                inventoryLog.Batch = item.Batch;
                inventoryLog.InQuantity = item.CheckQuantity;
                inventoryLog.OutQuantity = 0;
                var inventory = await _inventoryRepository.StorageAsync(item.LocationId, item.ProductId, item.CheckQuantity, item.Batch);
                inventoryLog.AfterQuantity = inventory.Quantity;
                await _inventoryLogRepository.InsertAsync(inventoryLog);
            }
            else if(item.CheckType == InventoryCheckDetailType.Reduce) //盘亏
            {
                if (item.CheckQuantity <= 0)
                {
                    throw new UserFriendlyException("盈亏数量必须大于0");
                }
                InventoryLog inventoryLog = new InventoryLog(GuidGenerator.Create());
                inventoryLog.Number = inventoryCheck.Number;
                inventoryLog.ProductId = item.ProductId;
                inventoryLog.LocationId = item.LocationId;
                inventoryLog.LogTime = Clock.Now;
                inventoryLog.LogType = InventoryLogType.InventoryCheckReduce;
                inventoryLog.Batch = item.Batch;
                inventoryLog.InQuantity = 0;
                inventoryLog.OutQuantity = item.CheckQuantity;
                double afterQuantity = await _inventoryRepository.OutAsync(item.LocationId, item.ProductId, item.CheckQuantity, item.Batch);
                inventoryLog.AfterQuantity = afterQuantity;
                await _inventoryLogRepository.InsertAsync(inventoryLog);
            }
            
        }

        await _inventoryCheckRepository.UpdateAsync(inventoryCheck);
    }


    [Authorize(ERPPermissions.InventoryCheck.Update)]
    public async Task UpdateAsync(Guid id, InventoryCheckUpdateDto input)
    {
        InventoryCheck inventoryCheck = await _inventoryCheckRepository.FindAsync(id);
        if (inventoryCheck == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //

        if (inventoryCheck.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经确认无法修改");
        }

        inventoryCheck.CheckDate = input.CheckDate;
        inventoryCheck.Discription = input.Discription;
        inventoryCheck.WarehouseId = input.WarehouseId;

        List<InventoryCheckDetail> createList = new List<InventoryCheckDetail>();
        List<InventoryCheckDetail> updateList = new List<InventoryCheckDetail>();
        List<InventoryCheckDetail> deleteList = new List<InventoryCheckDetail>();
        List<InventoryCheckDetail> dbList = await _inventoryCheckDetailRepository.GetListAsync(m => m.InventoryCheckId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                InventoryCheckDetail detail = new InventoryCheckDetail(detailId);
                detail.InventoryCheckId = id;
                detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.Batch = item.Batch;
                detail.InventoryQuantity = item.InventoryQuantity;
                detail.CheckType = item.CheckType;
                detail.CheckQuantity = item.CheckQuantity;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                InventoryCheckDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.Batch = item.Batch;
                detail.InventoryQuantity = item.InventoryQuantity;
                detail.CheckType = item.CheckType;
                detail.CheckQuantity = item.CheckQuantity;
                detail.Sort = i;
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

        var result = await _inventoryCheckRepository.UpdateAsync(inventoryCheck);
        await _inventoryCheckDetailRepository.InsertManyAsync(createList);
        await _inventoryCheckDetailRepository.UpdateManyAsync(updateList);
        await _inventoryCheckDetailRepository.DeleteManyAsync(deleteList);
    }
}
