using System;
using System.Threading.Tasks;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;

public interface IPurchaseReturnApplyAppService :IApplicationService
{
    Task<PurchaseReturnApplyDto> GetAsync(Guid id);

    Task<PagedResultDto<PurchaseReturnApplyDto>> GetPagedListAsync(PurchaseReturnApplyPagedRequestDto input);

    Task CreateAsync(PurchaseReturnApplyCreateDto input);

    Task UpdateAsync(Guid id, PurchaseReturnApplyUpdateDto input);

    Task DeleteAsync(Guid id);

    Task ConfirmAsync(Guid id);
}
