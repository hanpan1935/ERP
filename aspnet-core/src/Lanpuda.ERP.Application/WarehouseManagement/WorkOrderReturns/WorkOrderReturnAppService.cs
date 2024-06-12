using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;
using Lanpuda.ERP.Permissions;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;


[Authorize]
public class WorkOrderReturnAppService : ERPAppService, IWorkOrderReturnAppService
{
    private readonly IWorkOrderReturnRepository _workOrderReturnRepository;
    private readonly IWorkOrderReturnDetailRepository _workOrderReturnDetailRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;

    public WorkOrderReturnAppService(
        IWorkOrderReturnRepository repository,
        IWorkOrderReturnDetailRepository detailRepository,
        IInventoryRepository inventoryRepository,
        IInventoryLogRepository inventoryLogRepository
        )
    {
        _workOrderReturnRepository = repository;
        this._workOrderReturnDetailRepository = detailRepository;
        this._inventoryRepository = inventoryRepository;
        this._inventoryLogRepository = inventoryLogRepository;
    }


    [Authorize(ERPPermissions.WorkOrderReturn.Update)]
    public async Task<WorkOrderReturnDto> GetAsync(Guid id)
    {
        var entity = await _workOrderReturnRepository.GetAsync(id, true);
        if (entity == null)
        {
            throw new EntityNotFoundException();
        }
        var dto = ObjectMapper.Map<WorkOrderReturn, WorkOrderReturnDto>(entity);
        return dto;
    }



    [Authorize(ERPPermissions.WorkOrderReturn.Default)]
    public async Task<PagedResultDto<WorkOrderReturnDto>> GetPagedListAsync(WorkOrderReturnPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _workOrderReturnRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.MaterialReturnApplyDetail.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.Product.Name.Contains(input.ProductName))
            .WhereIf(!string.IsNullOrEmpty(input.MaterialReturnApplyNumber), m => m.MaterialReturnApplyDetail.MaterialReturnApply.Number.Contains(input.MaterialReturnApplyNumber))
            .WhereIf(!string.IsNullOrEmpty(input.WorkOrderNumber), m => m.MaterialReturnApplyDetail.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.MaterialApply.WorkOrder.Number.Contains(input.WorkOrderNumber))
            .WhereIf(input.IsSuccessful != null, m => m.IsSuccessful.Equals(input.IsSuccessful))
            ;

        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<WorkOrderReturnDto>(totalCount, ObjectMapper.Map<List<WorkOrderReturn>, List<WorkOrderReturnDto>>(result));
    }


    [Authorize(ERPPermissions.WorkOrderReturn.Storage)]
    public async Task StoragedAsync(Guid id)
    {
        var entity = await _workOrderReturnRepository.GetAsync(id, true);
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
                item.WorkOrderReturn.MaterialReturnApplyDetail.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.ProductId,
                item.Quantity,
                item.WorkOrderReturn.MaterialReturnApplyDetail.WorkOrderOutDetail.Batch);
            InventoryLog inventoryLog = new InventoryLog(GuidGenerator.Create());
            inventoryLog.AfterQuantity = inventory.Quantity;
            inventoryLog.Number = entity.Number;
            inventoryLog.ProductId = inventory.ProductId;
            inventoryLog.LocationId = item.LocationId;
            inventoryLog.LogTime = Clock.Now;
            inventoryLog.LogType = InventoryLogType.WorkOrderReturn;
            inventoryLog.Batch = inventory.Batch;
            inventoryLog.InQuantity = item.Quantity;
            inventoryLog.OutQuantity = 0;
            await _inventoryLogRepository.InsertAsync(inventoryLog);
        }
    }


    [Authorize(ERPPermissions.WorkOrderReturn.Update)]
    public async Task UpdateAsync(Guid id, WorkOrderReturnUpdateDto input)
    {
        WorkOrderReturn workOrderReturn = await _workOrderReturnRepository.FindAsync(id);
        if (workOrderReturn == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //

        if (workOrderReturn.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库,无法编辑");
        }


        workOrderReturn.Remark = input.Remark;

        List<WorkOrderReturnDetail> createList = new List<WorkOrderReturnDetail>();
        List<WorkOrderReturnDetail> updateList = new List<WorkOrderReturnDetail>();
        List<WorkOrderReturnDetail> deleteList = new List<WorkOrderReturnDetail>();
        List<WorkOrderReturnDetail> dbList = await _workOrderReturnDetailRepository.GetListAsync(m => m.WorkOrderReturnId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                WorkOrderReturnDetail detail = new WorkOrderReturnDetail(detailId);
                detail.WorkOrderReturnId = id;
                detail.LocationId = item.LocationId;
                detail.Quantity = item.Quantity;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                WorkOrderReturnDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.LocationId = item.LocationId;
                detail.Quantity = item.Quantity;
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

        var result = await _workOrderReturnRepository.UpdateAsync(workOrderReturn);
        await _workOrderReturnDetailRepository.InsertManyAsync(createList);
        await _workOrderReturnDetailRepository.UpdateManyAsync(updateList);
        await _workOrderReturnDetailRepository.DeleteManyAsync(deleteList);
    }
}
