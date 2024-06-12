using Lanpuda.ERP.BasicData.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.BasicData.Products
{
    public interface IProductLookupAppService : IApplicationService
    {
        Task<List<ProductDto>> GetAllAsync();

        Task<List<ProductDto>> GetForMpsAsync();

        Task<PagedResultDto<ProductDto>> GetPagedListForWorkOrderAsync(ProductPagedRequestDto input);

        Task<PagedResultDto<ProductWithPriceDto>> GetPagedListWithPurchasePriceAsync(ProductWithPurchasePricePagedRequestDto input);

        Task<PagedResultDto<ProductWithPriceDto>> GetPagedListWithSalesPriceAsync(ProductWithSalesPricePagedRequestDto input);
    }
}
