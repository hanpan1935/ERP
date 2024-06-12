using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.ProductionManagement.Workshops.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.ProductionManagement.Workshops;

public interface IWorkshopAppService : IApplicationService
{
    Task<WorkshopDto> GetAsync(Guid id);

    Task<PagedResultDto<WorkshopDto>> GetPagedListAsync(WorkshopPagedRequestDto input);

    Task CreateAsync(WorkshopCreateDto input);
         
    Task UpdateAsync(Guid id, WorkshopUpdateDto input);

    Task DeleteAsync(Guid id);

    Task<List<WorkshopDto>> GetAllAsync();
}