using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.Utils.UniqueCode;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Lanpuda.ERP.ProductionManagement.Boms.Dtos;
using Volo.Abp.Data;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Lanpuda.ERP.QualityManagement.ProcessInspections;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies;

[Authorize]
public class WorkOrderStorageApplyAppService : ERPAppService, IWorkOrderStorageApplyAppService
{
    private readonly IWorkOrderStorageApplyRepository _repository;
    private readonly IUniqueCodeUtils _uniqueCodeUtils;
    private readonly IWorkOrderStorageRepository _workOrderStorageRepository;
    private readonly IProcessInspectionRepository _processInspectionRepository;

    public WorkOrderStorageApplyAppService(
        IWorkOrderStorageApplyRepository repository,
        IUniqueCodeUtils uniqueCodeUtils,
        IProcessInspectionRepository processInspectionRepository,
        IWorkOrderStorageRepository workOrderStorageRepository)
    {

        _repository = repository;
        _uniqueCodeUtils = uniqueCodeUtils;
        _workOrderStorageRepository = workOrderStorageRepository;
        _processInspectionRepository = processInspectionRepository;


    }


    [Authorize(ERPPermissions.WorkOrderStorageApply.Create)]
    public async Task CreateAsync(WorkOrderStorageApplyCreateDto input)
    {

        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.WorkOrderStorageApplysPrefix); 

        WorkOrderStorageApply workOrderStorageApply = new WorkOrderStorageApply(id);
        workOrderStorageApply.Number = number;
        workOrderStorageApply.WorkOrderId = input.WorkOrderId;
        workOrderStorageApply.Quantity = input.Quantity;
        workOrderStorageApply.Remark = input.Remark;
        workOrderStorageApply.IsConfirmed = false;

      
        WorkOrderStorageApply result = await _repository.InsertAsync(workOrderStorageApply);
    }


    [Authorize(ERPPermissions.WorkOrderStorageApply.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        WorkOrderStorageApply workOrderStorageApply = await _repository.FindAsync(id);
        if (workOrderStorageApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        var storage = await _workOrderStorageRepository.FirstOrDefaultAsync(m=>m.WorkOrderStorageApplyId == id);

        if (storage != null) 
        {
            if (storage.IsSuccessful == true)
            {
                throw new UserFriendlyException("已经入库,无法删除!");
            }
            await _workOrderStorageRepository.DeleteAsync(storage);
        }

        var processInspection = await _processInspectionRepository.FirstOrDefaultAsync(m => m.WorkOrderStorageApplyId == id);
        if (processInspection != null)
        {
            await _processInspectionRepository.DeleteAsync(processInspection);
        }
        await _repository.DeleteAsync(workOrderStorageApply);
    }


    [Authorize(ERPPermissions.WorkOrderStorageApply.Confirm)]
    public async Task ConfirmeAsync(Guid id)
    {
        WorkOrderStorageApply workOrderStorageApply = await _repository.FindAsync(id);
        if (workOrderStorageApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (workOrderStorageApply.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认了!");
        }

        if (workOrderStorageApply.Quantity <= 0)
        {
            throw new UserFriendlyException("数量必须大于0");
        }


        workOrderStorageApply.IsConfirmed = true;
        workOrderStorageApply.ConfirmedTime = Clock.Now;
        workOrderStorageApply.ConfirmedUserId = CurrentUser.Id;
        //创建工单入库单

        WorkOrderStorage workOrderStorage = new WorkOrderStorage(GuidGenerator.Create());
        workOrderStorage.WorkOrderStorageApplyId = id;
        workOrderStorage.Number = await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.WorkOrderStoragePrefix);
        workOrderStorage.IsSuccessful = false;
        await _workOrderStorageRepository.InsertAsync(workOrderStorage);


        //创建过程检验

        if (workOrderStorageApply.WorkOrder.Product.IsProcessInspection == true)
        {
            ProcessInspection processInspection = new ProcessInspection(GuidGenerator.Create());
            processInspection.WorkOrderStorageApplyId = workOrderStorageApply.Id;
            processInspection.Number =await _uniqueCodeUtils.GetUniqueNumberAsync(ERPConsts.ProcessInspectionPrefix);
            await _processInspectionRepository.InsertAsync(processInspection);
        }
    }


    [Authorize(ERPPermissions.WorkOrderStorageApply.Update)]
    public async Task<WorkOrderStorageApplyDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id, true);
        return ObjectMapper.Map<WorkOrderStorageApply, WorkOrderStorageApplyDto>(result);
    }

    [Authorize(ERPPermissions.WorkOrderStorageApply.Default)]
    public async Task<PagedResultDto<WorkOrderStorageApplyDto>> GetPagedListAsync(WorkOrderStorageApplyPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
               .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
               .WhereIf(!string.IsNullOrEmpty(input.WorkOrderNumber), m => m.WorkOrder.Number.Contains(input.WorkOrderNumber))
               .WhereIf(input.IsConfirmed != null, m => m.IsConfirmed.Equals(input.IsConfirmed))
               ;
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<WorkOrderStorageApplyDto>(totalCount, ObjectMapper.Map<List<WorkOrderStorageApply>, List<WorkOrderStorageApplyDto>>(result));
    }


    [Authorize(ERPPermissions.WorkOrderStorageApply.Update)]
    public async Task UpdateAsync(Guid id, WorkOrderStorageApplyUpdateDto input)
    {
        WorkOrderStorageApply workOrderStorageApply = await _repository.FindAsync(id);
        if (workOrderStorageApply == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (workOrderStorageApply.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认-无法修改!");
        }


        workOrderStorageApply.WorkOrderId = input.WorkOrderId;
        workOrderStorageApply.Quantity = input.Quantity;
        workOrderStorageApply.Remark = input.Remark;
        var result = await _repository.UpdateAsync(workOrderStorageApply);
    }
    
}
