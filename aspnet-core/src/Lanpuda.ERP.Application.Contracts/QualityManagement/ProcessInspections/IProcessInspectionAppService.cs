using System;
using Lanpuda.ERP.QualityManagement.ProcessInspections.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.QualityManagement.ProcessInspections;

public interface IProcessInspectionAppService :IApplicationService
{
    Task<ProcessInspectionDto> GetAsync(Guid id);

    Task<PagedResultDto<ProcessInspectionDto>> GetPagedListAsync(ProcessInspectionPagedRequestDto input);

    Task UpdateAsync(Guid id, ProcessInspectionUpdateDto input);

    Task ConfirmeAsync(Guid id);
}