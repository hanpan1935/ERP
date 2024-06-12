using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.BasicData.Products;

public interface IProductAppService :IApplicationService
{
    Task<ProductDto> GetAsync(Guid id);

    Task CreateAsync(ProductCreateDto input);

    Task<PagedResultDto<ProductDto>> GetPagedListAsync(ProductPagedRequestDto input);

    Task UpdateAsync(Guid id, ProductUpdateDto input);

    Task DeleteAsync(Guid id);

}