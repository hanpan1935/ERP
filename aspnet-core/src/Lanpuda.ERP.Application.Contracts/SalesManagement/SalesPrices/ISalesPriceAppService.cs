using System;
using System.Threading.Tasks;
using Lanpuda.ERP.SalesManagement.SalesPrices.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.SalesManagement.SalesPrices;

public interface ISalesPriceAppService : IApplicationService
{
    Task<SalesPriceDto> GetAsync(Guid id);

    Task<PagedResultDto<SalesPriceDto>> GetPagedListAsync(SalesPricePagedRequestDto input);

    Task CreateAsync(SalesPriceCreateDto input);

    Task UpdateAsync(Guid id, SalesPriceUpdateDto input);

    Task DeleteAsync(Guid id);
}