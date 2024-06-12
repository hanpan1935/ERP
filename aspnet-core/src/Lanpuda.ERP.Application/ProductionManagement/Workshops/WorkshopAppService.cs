using System;
using Lanpuda.ERP.ProductionManagement.Workshops.Dtos;
using Lanpuda.ERP.ProductionManagement.Workshops;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.ERP.ProductionManagement.Workshops;

[Authorize]
public class WorkshopAppService : ERPAppService, IWorkshopAppService
{
    private readonly IWorkshopRepository _repository;

    public WorkshopAppService(IWorkshopRepository repository)
    {
        _repository = repository;
    }

    [Authorize(ERPPermissions.Workshop.Create)]
    public async Task CreateAsync(WorkshopCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        Workshop workshop = new Workshop(id);
        workshop.Number = input.Number;
        workshop.Name = input.Name;
        workshop.Remark = input.Remark;

        Workshop result = await _repository.InsertAsync(workshop);
    }

    [Authorize(ERPPermissions.Workshop.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Workshop workshop = await _repository.FindAsync(id);
        if (workshop == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _repository.DeleteAsync(workshop);
    }

    [Authorize(ERPPermissions.Workshop.Update)]
    public async Task<WorkshopDto> GetAsync(Guid id)
    {
        var result = await _repository.FindAsync(id);
        return ObjectMapper.Map<Workshop, WorkshopDto>(result);
    }

    [Authorize(ERPPermissions.Workshop.Default)]
    public async Task<PagedResultDto<WorkshopDto>> GetPagedListAsync(WorkshopPagedRequestDto input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _repository.WithDetailsAsync();
        query = query
            .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
            .WhereIf(!string.IsNullOrEmpty(input.Name), m => m.Name.Contains(input.Name));
        long totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<WorkshopDto>(totalCount, ObjectMapper.Map<List<Workshop>, List<WorkshopDto>>(result));
    }


    [Authorize(ERPPermissions.Workshop.Update)]
    public async Task UpdateAsync(Guid id, WorkshopUpdateDto input)
    {
        Workshop workshop = await _repository.FindAsync(id);
        if (workshop == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        workshop.Name = input.Name;
        workshop.Number = input.Number;
        workshop.Remark = input.Remark;
        var result = await _repository.UpdateAsync(workshop);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<List<WorkshopDto>> GetAllAsync()
    {
        var result = await _repository.GetListAsync();
        return ObjectMapper.Map<List<Workshop>, List<WorkshopDto>>(result);
    }
}
