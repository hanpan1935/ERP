using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.ProductionManagement.Boms;
using Lanpuda.ERP.ProductionManagement.Boms.Dtos;
using Lanpuda.ERP.ProductionManagement.MaterialApplies;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;
using Lanpuda.ERP.ProductionManagement.Mpses;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies;
using Lanpuda.ERP.QualityManagement.ProcessInspections;
using Lanpuda.ERP.Utils.UniqueCode;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders;

[Authorize]
public class WorkOrderAppService : ERPAppService, IWorkOrderAppService
{
    private readonly IWorkOrderRepository _workOrderRepository;
    private readonly IWorkOrderMaterialRepository _workOrderMaterialRepository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly IWorkOrderStorageRepository _workOrderStorageRepository;
    private readonly IMpsRepository _mpsRepository;
    private readonly IBomRepository _bomRepository;
    private readonly IMaterialApplyRepository _materialApplyRepository;
    private readonly IMaterialReturnApplyRepository _materialReturnApplyRepository;
    private readonly IWorkOrderStorageApplyRepository _workOrderStorageApplyRepository;
    private readonly WorkOrderManager _workOrderManager;


    public WorkOrderAppService(
        IWorkOrderRepository workOrderRepository,
        IWorkOrderMaterialRepository workOrderMaterialRepository,
        IWorkOrderStorageRepository workOrderStorageRepository,
        IMaterialApplyRepository materialApplyRepository,
        IMaterialReturnApplyRepository materialReturnApplyRepository,
        IWorkOrderStorageApplyRepository workOrderStorageApplyRepository,
        IMpsRepository mpsRepository,
        IBomRepository bomRepository,
        WorkOrderManager workOrderManager,
        IUniqueCodeUtils uniqueCodeUtils)
    {
        _workOrderRepository = workOrderRepository;
        _workOrderMaterialRepository = workOrderMaterialRepository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _workOrderStorageRepository = workOrderStorageRepository;
        _mpsRepository = mpsRepository;
        _bomRepository = bomRepository;
        _materialApplyRepository = materialApplyRepository;
        _materialReturnApplyRepository = materialReturnApplyRepository;
        _workOrderStorageApplyRepository = workOrderStorageApplyRepository;
        _workOrderManager = workOrderManager;
    }

    [Authorize(ERPPermissions.WorkOrder.Create)]
    public async Task MultipleCreateAsync(WorkOrderMultipleCreateDto input)
    {
        List<WorkOrder> workOrders = new List<WorkOrder>();
        List<WorkOrderMaterial> materials = new List<WorkOrderMaterial>();
        foreach (var item in input.Details)
        {
            Guid id = GuidGenerator.Create();
            string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.WorkOrderPrefix);
            WorkOrder workOrder = new WorkOrder(id);
            workOrder.Number = number;
            workOrder.WorkshopId = item.WorkshopId;
            workOrder.MpsId = input.MpsId;
            workOrder.ProductId = item.ProductId;
            workOrder.Quantity = item.Quantity;
            workOrder.StartDate = item.StartDate;
            workOrder.CompletionDate = item.CompletionDate;
            workOrder.Remark = item.Remark;
            workOrders.Add(workOrder);
            var bomdetail = await _workOrderManager.GetBomDetailAsync(workOrder);
            materials.AddRange(bomdetail);
        }
        await _workOrderRepository.InsertManyAsync(workOrders);
        await _workOrderMaterialRepository.InsertManyAsync(materials);
    }


    [Authorize(ERPPermissions.WorkOrder.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        WorkOrder workOrder = await _workOrderRepository.FindAsync(id);
        if (workOrder == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //确认有没有领料申请
        var hasMaterialApply = await _materialApplyRepository.AnyAsync(m=>m.WorkOrderId == workOrder.Id);
        if (hasMaterialApply)
        {
            throw new UserFriendlyException("请先删除对应的领料申请单");
        }

        //确认有没有入库申请
        var hasWorkOrderStorageApply = await _workOrderStorageApplyRepository.AnyAsync(m => m.WorkOrderId == workOrder.Id);
        if (hasWorkOrderStorageApply)
        {
            throw new UserFriendlyException("请先删除对应的入库申请单");
        }

        await _workOrderRepository.DeleteAsync(workOrder);
    }



    [Authorize(ERPPermissions.WorkOrder.Update)]
    public async Task<WorkOrderDto> GetAsync(Guid id)
    {
        var result = await _workOrderRepository.FindAsync(id, true);
        return ObjectMapper.Map<WorkOrder, WorkOrderDto>(result);
    }


    [Authorize(ERPPermissions.WorkOrder.Default)]
    public async Task<PagedResultDto<WorkOrderDto>> GetPagedListAsync(WorkOrderPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _workOrderRepository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.MpsNumber), m => m.Mps.Number.Contains(input.MpsNumber))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), m => m.Product.Name.Contains(input.ProductName))
            .WhereIf(input.IsConfirmed != null, m => m.IsConfirmed.Equals(input.IsConfirmed))
            .WhereIf(input.StartDate != null, m => m.StartDate.Equals(input.StartDate))
            .WhereIf(input.CompletionDate != null, m => m.CompletionDate.Equals(input.CompletionDate))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<WorkOrderDto>(totalCount, ObjectMapper.Map<List<WorkOrder>, List<WorkOrderDto>>(result));
    }


    [Authorize(ERPPermissions.WorkOrder.Update)]
    public async Task UpdateAsync(Guid id, WorkOrderUpdateDto input)
    {
        WorkOrder workOrder = await _workOrderRepository.FindAsync(id, false);
        if (workOrder == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (workOrder.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认！无法修改！");
        }

        workOrder.MpsId = input.MpsId;
        workOrder.WorkshopId = input.WorkshopId;
        workOrder.ProductId = input.ProductId;
        workOrder.Quantity = input.Quantity;
        workOrder.StartDate = input.StartDate;
        workOrder.CompletionDate = input.CompletionDate;
        workOrder.Remark = input.Remark;

        var materialList = await _workOrderManager.GetBomDetailAsync(workOrder);
        workOrder.StandardMaterialDetails = materialList;
        var result = await _workOrderRepository.UpdateAsync(workOrder);
    }


    [Authorize(ERPPermissions.WorkOrder.Confirm)]
    public async Task ConfirmeAsync(Guid id)
    {
        WorkOrder workOrder = await _workOrderRepository.FindAsync(id);
        if (workOrder == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (workOrder.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认了，无法再次确认");
        }

        if (workOrder.Quantity <= 0)
        {
            throw new UserFriendlyException("生产数量不能小于或者等于0");
        }

        workOrder.IsConfirmed = true;
        workOrder.ConfirmedUserId = CurrentUser.Id;
        workOrder.ConfirmedTime = Clock.Now;
        await _workOrderRepository.UpdateAsync(workOrder);
    }


    [Authorize(ERPPermissions.WorkOrder.Confirm)]
    public async Task MultipleConfirmeAsync(List<Guid> ids)
    {
        var queryable = await _workOrderRepository.WithDetailsAsync();
        queryable = from m in queryable
                    where ids.Contains(m.Id)
                    select m;
        var workOrders = await AsyncExecuter.ToListAsync(queryable);

        foreach (var workOrder in workOrders)
        {
            if (workOrder == null)
            {
                throw new EntityNotFoundException(L["Message:DoesNotExist"]);
            }

            if (workOrder.IsConfirmed == true)
            {
                throw new UserFriendlyException("已经确认了，无法再次确认");
            }

            if (workOrder.Quantity <= 0)
            {
                throw new UserFriendlyException("生产数量不能小于或者等于0");
            }

            workOrder.IsConfirmed = true;
            workOrder.ConfirmedTime = Clock.Now;
            workOrder.ConfirmedUserId = CurrentUser.Id;
        }
        await _workOrderRepository.UpdateManyAsync(workOrders);
    }


}
