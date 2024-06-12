using System;
using System.Threading.Tasks;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies;

public interface IPurchaseApplyAppService : IApplicationService
{
    Task<PurchaseApplyDto> GetAsync(Guid id);

    Task<PagedResultDto<PurchaseApplyDto>> GetPagedListAsync(PurchaseApplyGetListInput input);

    Task CreateAsync(PurchaseApplyCreateDto input);

    Task UpdateAsync(Guid id, PurchaseApplyUpdateDto input);

    Task DeleteAsync(Guid id);

    Task ConfirmeAsync(Guid id);

    Task AcceptAsync(Guid id);
}