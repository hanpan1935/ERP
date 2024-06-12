using System;
using System.Threading.Tasks;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices;

public interface IArrivalNoticeAppService : IApplicationService
{
    Task<ArrivalNoticeDto> GetAsync(Guid id);

    Task<PagedResultDto<ArrivalNoticeDto>> GetPagedListAsync(ArrivalNoticePagedRequestDto input);

    Task CreateAsync(ArrivalNoticeCreateDto input);

    Task UpdateAsync(Guid id, ArrivalNoticeUpdateDto input);

    Task DeleteAsync(Guid id);

    Task<ArrivalNoticeDto> ConfirmeAsync(Guid id);
}