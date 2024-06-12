using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Lanpuda.ERP.QualityManagement.ProcessInspections.Dtos;
using Lanpuda.ERP.QualityManagement.ProcessInspections;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.QualityManagement.ProcessInspections;

[Authorize]
public class ProcessInspectionAppService : ERPAppService, IProcessInspectionAppService
{
    private readonly IProcessInspectionRepository _processInspectionRepository;

    public ProcessInspectionAppService(IProcessInspectionRepository processInspectionRepository)
    {
        _processInspectionRepository = processInspectionRepository;
    }



    [Authorize(ERPPermissions.ProcessInspection.Confirm)]
    public async Task ConfirmeAsync(Guid id)
    {
        ProcessInspection processInspection = await _processInspectionRepository.FindAsync(id);
        if (processInspection == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (processInspection.IsConfirmed == true)
        {
            throw new UserFriendlyException("已经确认过了");
        }
        processInspection.IsConfirmed = true;
        processInspection.ConfirmedTime = Clock.Now;
        processInspection.ConfirmeUserId = CurrentUser.Id;
        await _processInspectionRepository.UpdateAsync(processInspection);
    }



    [Authorize(ERPPermissions.ProcessInspection.Update)]
    public async Task<ProcessInspectionDto> GetAsync(Guid id)
    {
        var result = await _processInspectionRepository.FindAsync(id);
        return ObjectMapper.Map<ProcessInspection, ProcessInspectionDto>(result);
    }



    [Authorize(ERPPermissions.ProcessInspection.Default)]
    public async Task<PagedResultDto<ProcessInspectionDto>> GetPagedListAsync(ProcessInspectionPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _processInspectionRepository.WithDetailsAsync();

        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), x => x.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.WorkOrderNumber), x => x.WorkOrderStorageApply.WorkOrder.Number.Contains(input.WorkOrderNumber))
            .WhereIf(!string.IsNullOrEmpty(input.ProductName), x => x.WorkOrderStorageApply.WorkOrder.Product.Name.Contains(input.ProductName))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<ProcessInspectionDto>(totalCount, ObjectMapper.Map<List<ProcessInspection>, List<ProcessInspectionDto>>(result));
    }


    [Authorize(ERPPermissions.ProcessInspection.Update)]
    public async Task UpdateAsync(Guid id, ProcessInspectionUpdateDto input)
    {
        ProcessInspection processInspection = await _processInspectionRepository.FindAsync(id);
        if (processInspection == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        processInspection.BadQuantity = input.BadQuantity;
        processInspection.Description = input.Description;
        var result = await _processInspectionRepository.UpdateAsync(processInspection);
    }
}
