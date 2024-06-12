using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;

[Authorize]
public class WorkOrderOutAppService : ERPAppService, IWorkOrderOutAppService
{
    private readonly IWorkOrderOutRepository _workOrderOutRepository;
    private readonly IWorkOrderOutDetailRepository _workOrderOutDetailRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;


    public WorkOrderOutAppService(
        IWorkOrderOutRepository repository,
        IWorkOrderOutDetailRepository detailRepository,
        IInventoryRepository inventoryRepository,
        IInventoryLogRepository inventoryLogRepository)
    {
        _workOrderOutRepository = repository;
        _workOrderOutDetailRepository = detailRepository;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
    }



    [Authorize(ERPPermissions.WorkOrderOut.Update)]
    public async Task<WorkOrderOutDto> GetAsync(Guid id)
    {
        var entity = await _workOrderOutRepository.GetAsync(id, true);
        var dto = ObjectMapper.Map<WorkOrderOut, WorkOrderOutDto>(entity);
        return dto;
    }



    [Authorize(ERPPermissions.WorkOrderOut.Default)]
    public async Task<PagedResultDto<WorkOrderOutDto>> GetPagedListAsync(WorkOrderOutPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _workOrderOutRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.MaterialApplyDetail.Product.Name.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.MaterialApplyNumber), m => m.MaterialApplyDetail.MaterialApply.Number.Contains(input.MaterialApplyNumber))
            .WhereIf(!string.IsNullOrEmpty(input.WorkOrderNumber), m => m.MaterialApplyDetail.MaterialApply.WorkOrder.Number.Contains(input.WorkOrderNumber))
            .WhereIf(input.IsSuccessful != null, m => m.IsSuccessful.Equals(input.IsSuccessful))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<WorkOrderOutDto>(totalCount, ObjectMapper.Map<List<WorkOrderOut>, List<WorkOrderOutDto>>(result));
    }



    [Authorize(ERPPermissions.WorkOrderOut.Out)]
    public async Task OutedAsync(Guid id)
    {
        var workOrderOut = await _workOrderOutRepository.GetAsync(id, true);

        if (workOrderOut == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (workOrderOut.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库");
        }

        if (workOrderOut.Details == null || workOrderOut.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }

        foreach (var item in workOrderOut.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }


        workOrderOut.SuccessfulTime = Clock.Now;
        workOrderOut.IsSuccessful = true;
        workOrderOut.KeeperUserId = CurrentUser.Id;

        foreach (var item in workOrderOut.Details)
        {
            double quantity = await  _inventoryRepository.OutAsync(
                item.LocationId, 
                item.WorkOrderOut.MaterialApplyDetail.ProductId, 
                item.Quantity, 
                item.Batch);
            var inventoryLog = new InventoryLog(GuidGenerator.Create());
            inventoryLog.Number = workOrderOut.Number;
            inventoryLog.ProductId = item.WorkOrderOut.MaterialApplyDetail.ProductId;
            inventoryLog.LocationId = item.LocationId;
            inventoryLog.LogTime = Clock.Now;
            inventoryLog.LogType = InventoryLogType.WorkOrderOut;
            inventoryLog.Batch = item.Batch;
            inventoryLog.InQuantity = 0;
            inventoryLog.OutQuantity = item.Quantity;
            inventoryLog.AfterQuantity = quantity;
            await _inventoryLogRepository.InsertAsync(inventoryLog);
        }
        await _workOrderOutRepository.UpdateAsync(workOrderOut);
    }


    [Authorize(ERPPermissions.WorkOrderOut.Update)]
    public async Task UpdateAsync(Guid id, WorkOrderOutUpdateDto input)
    {
        WorkOrderOut workOrderOut = await _workOrderOutRepository.FindAsync(id);
        if (workOrderOut == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //
        if (workOrderOut.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库,无法编辑");
        }
     


        workOrderOut.Remark = input.Remark;

        List<WorkOrderOutDetail> createList = new List<WorkOrderOutDetail>();
        List<WorkOrderOutDetail> updateList = new List<WorkOrderOutDetail>();
        List<WorkOrderOutDetail> deleteList = new List<WorkOrderOutDetail>();
        List<WorkOrderOutDetail> dbList = await _workOrderOutDetailRepository.GetListAsync(m => m.WorkOrderOutId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                WorkOrderOutDetail detail = new WorkOrderOutDetail(detailId);
                detail.WorkOrderOutId = id;
                detail.LocationId = item.LocationId;
                detail.Batch = item.Batch;
                detail.Quantity = item.Quantity;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                WorkOrderOutDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.LocationId = item.LocationId;
                detail.Batch = item.Batch;
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

        var result = await _workOrderOutRepository.UpdateAsync(workOrderOut);
        await _workOrderOutDetailRepository.InsertManyAsync(createList);
        await _workOrderOutDetailRepository.UpdateManyAsync(updateList);
        await _workOrderOutDetailRepository.DeleteManyAsync(deleteList);
    }


    /// <summary>
    /// 用于生产退料
    /// </summary>
    /// <param name="id">workorderId</param>
    /// <returns></returns>
    /// 

    [Authorize(ERPPermissions.MaterialReturnApply.Create)]
    [Authorize(ERPPermissions.MaterialReturnApply.Update)]
    public async Task<PagedResultDto<WorkOrderOutDetailSelectDto>> GetDetailPagedListAsync(WorkOrderOutDetailPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _workOrderOutDetailRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.WorkOrderOutNumber), m => m.WorkOrderOut.Number.Contains(input.WorkOrderOutNumber))
            .WhereIf(!string.IsNullOrEmpty(input.WorkOrderNumber), m => m.WorkOrderOut.MaterialApplyDetail.MaterialApply.WorkOrder.Number.Contains(input.WorkOrderNumber))
            .WhereIf(!string.IsNullOrEmpty(input.MaterialApplyNumber), m => m.WorkOrderOut.MaterialApplyDetail.MaterialApply.Number.Contains(input.MaterialApplyNumber))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.WorkOrderOut.MaterialApplyDetail.Product.Name.Contains(input.ProductName))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        var detailDtoList = ObjectMapper.Map<List<WorkOrderOutDetail>, List<WorkOrderOutDetailSelectDto>>(result);
        return new PagedResultDto<WorkOrderOutDetailSelectDto>(totalCount, detailDtoList);
    }


    [Authorize(ERPPermissions.WorkOrderOut.Update)]
    public async Task<List<WorkOrderOutDetailDto>> AutoOutAsync(Guid id)
    {
        var entity = await _workOrderOutRepository.GetAsync(id, true);

        var query = await _inventoryRepository.WithDetailsAsync();
        query = query.Where(m => m.ProductId == entity.MaterialApplyDetail.ProductId).OrderBy(m => m.CreationTime);

        List<Inventory> inventories = await AsyncExecuter.ToListAsync(query);

        double totalNeedQuantity = entity.MaterialApplyDetail.Quantity;
        
        List<WorkOrderOutDetailDto> workOrderOutDetailDtos = new List<WorkOrderOutDetailDto>();
        foreach (var item in inventories)
        {
            //已经添加的数量
            double hasOutQuanity = workOrderOutDetailDtos.Sum(m => m.Quantity); 
            //还剩多少需要领
            double currentNeedQuantity = totalNeedQuantity - hasOutQuanity;
            WorkOrderOutDetailDto workOrderOutDetailDto = new WorkOrderOutDetailDto();
            workOrderOutDetailDto.WorkOrderOutId = entity.Id;
            workOrderOutDetailDto.WarehouseName = item.Location.Warehouse.Name;
            workOrderOutDetailDto.LocationName = item.Location.Name;
            workOrderOutDetailDto.LocationId = item.LocationId;
            workOrderOutDetailDto.Batch = item.Batch;
            //计算数量,如何够用
            if (item.Quantity >= currentNeedQuantity)
            {
                workOrderOutDetailDto.Quantity = currentNeedQuantity;
                workOrderOutDetailDtos.Add(workOrderOutDetailDto);
                break;
            }
            else
            {
                workOrderOutDetailDto.Quantity = item.Quantity;
                workOrderOutDetailDtos.Add(workOrderOutDetailDto);
            }
        }

        return workOrderOutDetailDtos;
    }
}
