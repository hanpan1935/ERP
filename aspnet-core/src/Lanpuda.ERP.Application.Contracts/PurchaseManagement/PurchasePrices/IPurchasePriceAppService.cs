using System;
using System.Threading.Tasks;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices;

public interface IPurchasePriceAppService : IApplicationService
{
    Task<PurchasePriceDto> GetAsync(Guid id);

    Task<PagedResultDto<PurchasePriceDto>> GetPagedListAsync(PurchasePricePagedRequestDto input);

    Task CreateAsync(PurchasePriceCreateDto input);

    Task UpdateAsync(Guid id, PurchasePriceUpdateDto input);

    Task DeleteAsync(Guid id);
}