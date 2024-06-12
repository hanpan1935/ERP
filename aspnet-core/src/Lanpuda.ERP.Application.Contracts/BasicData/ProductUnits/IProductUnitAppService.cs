using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.BasicData.ProductUnits.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;


namespace Lanpuda.ERP.BasicData.ProductUnits;

public interface IProductUnitAppService : IApplicationService
{
    Task<ProductUnitDto> GetAsync(Guid id);

    Task<PagedResultDto<ProductUnitDto>> GetPagedListAsync(ProductUnitPagedRequestDto input);

    Task CreateAsync(ProductUnitCreateDto input);

    Task UpdateAsync(Guid id, ProductUnitUpdateDto input);

    Task DeleteAsync(Guid id);

    Task<List<ProductUnitDto>> GetAllAsync();
}