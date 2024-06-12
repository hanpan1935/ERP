using System;
using System.Threading.Tasks;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;
using Lanpuda.ERP.QualityManagement.ArrivalInspections.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.QualityManagement.ArrivalInspections;

public interface IArrivalInspectionAppService : IApplicationService
{
    Task<ArrivalInspectionDto> GetAsync(Guid id);

    Task<PagedResultDto<ArrivalInspectionDto>> GetPagedListAsync(ArrivalInspectionPagedRequestDto input);
    
    Task UpdateAsync(Guid id, ArrivalInspectionUpdateDto input);

    Task ConfirmeAsync(Guid id);
}