using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;

[Authorize]
public class WorkOrderStorageAppService : ERPAppService, IWorkOrderStorageAppService
{
    private readonly IWorkOrderStorageRepository _workOrderStorageRepository;
    private readonly IWorkOrderStorageDetailRepository _workOrderStorageDetailRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;

    public WorkOrderStorageAppService(
        IWorkOrderStorageRepository repository,
        IWorkOrderStorageDetailRepository detailRepository,
        IInventoryRepository inventoryRepository,
        IInventoryLogRepository inventoryLogRepository
        )
    {
        _workOrderStorageRepository = repository;
        _workOrderStorageDetailRepository = detailRepository;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
    }


    [Authorize(ERPPermissions.WorkOrderStorage.Update)]
    public async Task<WorkOrderStorageDto> GetAsync(Guid id)
    {
        var entity = await _workOrderStorageRepository.GetAsync(id, true);
        var dto = ObjectMapper.Map<WorkOrderStorage, WorkOrderStorageDto>(entity);
        return dto;
    }



    [Authorize(ERPPermissions.WorkOrderStorage.Default)]
    public async Task<PagedResultDto<WorkOrderStorageDto>> GetPagedListAsync(WorkOrderStoragePagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _workOrderStorageRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.ApplyNumber), m => m.WorkOrderStorageApply.Number.Contains(input.ApplyNumber))
            .WhereIf(!string.IsNullOrEmpty(input.WorkOrderNumber), m => m.WorkOrderStorageApply.WorkOrder.Number.Contains(input.WorkOrderNumber))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.WorkOrderStorageApply.WorkOrder.Product.Name.Contains(input.ProductName))
            .WhereIf(input.IsSuccessful != null, m => m.IsSuccessful.Equals(input.IsSuccessful))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<WorkOrderStorageDto>(totalCount, ObjectMapper.Map<List<WorkOrderStorage>, List<WorkOrderStorageDto>>(result));
    }



    [Authorize(ERPPermissions.WorkOrderStorage.Storage)]
    public async Task StoragedAsync(Guid id)
    {
        var entity = await _workOrderStorageRepository.GetAsync(id, true);
        if (entity == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (entity.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库");
        }

        if (entity.Details == null || entity.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }

        foreach (var item in entity.Details)
        {
            if (item.Quantity < 0)
            {
                throw new UserFriendlyException("数量不能小于0");
            }
        }

        entity.SuccessfulTime = Clock.Now;
        entity.IsSuccessful = true;
        entity.KeeperUserId = CurrentUser.Id;

        foreach (var item in entity.Details)
        {
            var inventory = await _inventoryRepository.StorageAsync(
                item.LocationId, 
                item.WorkOrderStorage.WorkOrderStorageApply.WorkOrder.ProductId, 
                item.Quantity,
                item.WorkOrderStorage.WorkOrderStorageApply.Number);
            InventoryLog inventoryLog = new InventoryLog(GuidGenerator.Create());
            inventoryLog.Number = entity.Number;
            inventoryLog.ProductId = inventory.ProductId;
            inventoryLog.LocationId = item.LocationId;
            inventoryLog.LogTime = Clock.Now;
            inventoryLog.LogType = InventoryLogType.WorkOrderStorage;
            inventoryLog.Batch = inventory.Batch;
            inventoryLog.InQuantity = item.Quantity; 
            inventoryLog.OutQuantity = 0;
            inventoryLog.AfterQuantity = inventory.Quantity;
            await _inventoryLogRepository.InsertAsync(inventoryLog);
        }
    }


    [Authorize(ERPPermissions.WorkOrderStorage.Update)]
    public async Task UpdateAsync(Guid id, WorkOrderStorageUpdateDto input)
    {
        WorkOrderStorage workOrderStorage = await _workOrderStorageRepository.FindAsync(id, true);
        if (workOrderStorage == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //

        if (workOrderStorage.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库,无法编辑");
        }

        workOrderStorage.Remark = input.Remark;

        List<WorkOrderStorageDetail> createList = new List<WorkOrderStorageDetail>();
        List<WorkOrderStorageDetail> updateList = new List<WorkOrderStorageDetail>();
        List<WorkOrderStorageDetail> deleteList = new List<WorkOrderStorageDetail>();
        List<WorkOrderStorageDetail> dbList = await _workOrderStorageDetailRepository.GetListAsync(m => m.WorkOrderStorageId == id, true);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                WorkOrderStorageDetail detail = new WorkOrderStorageDetail(detailId);
                detail.WorkOrderStorageId = id;
                detail.LocationId = item.LocationId;
                detail.Quantity = item.Quantity;
                createList.Add(detail);
            }
            else //编辑
            {
                WorkOrderStorageDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.LocationId = item.LocationId;
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

        var result = await _workOrderStorageRepository.UpdateAsync(workOrderStorage);
        await _workOrderStorageDetailRepository.InsertManyAsync(createList);
        await _workOrderStorageDetailRepository.UpdateManyAsync(updateList);
        await _workOrderStorageDetailRepository.DeleteManyAsync(deleteList);
    }
}
