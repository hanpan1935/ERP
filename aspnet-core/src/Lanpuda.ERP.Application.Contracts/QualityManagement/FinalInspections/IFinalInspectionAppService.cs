using System;
using Lanpuda.ERP.QualityManagement.FinalInspections.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.QualityManagement.FinalInspections;

public interface IFinalInspectionAppService :IApplicationService
{
    Task<FinalInspectionDto> GetAsync(Guid id);

    Task<PagedResultDto<FinalInspectionDto>> GetPagedListAsync(FinalInspectionPagedRequestDto input);

    Task UpdateAsync(Guid id, FinalInspectionUpdateDto input);

    Task ConfirmeAsync(Guid id);
}