using Lanpuda.ERP.BasicData.ProductCategories.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.BasicData.ProductCategories;

public interface IProductCategoryAppService : IApplicationService
{
    Task<ProductCategoryDto> GetAsync(Guid id);

    Task<PagedResultDto<ProductCategoryDto>> GetPagedListAsync(ProductCategoryPagedRequestDto input);

    Task CreateAsync(ProductCategoryCreateDto input);

    Task UpdateAsync(Guid id, ProductCategoryUpdateDto input);

    Task DeleteAsync(Guid id);


    Task<List<ProductCategoryDto>> GetAllAsync();
}