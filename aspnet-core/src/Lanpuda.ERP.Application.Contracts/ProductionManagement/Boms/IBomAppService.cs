using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.ProductionManagement.Boms.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.ProductionManagement.Boms;

public interface IBomAppService : IApplicationService
{
    Task<BomDto> GetAsync(Guid id);

    Task<PagedResultDto<BomDto>> GetPagedListAsync(BomPagedRequestDto input);

    Task CreateAsync(BomCreateDto input);

    Task UpdateAsync(Guid id, BomUpdateDto input);

    Task DeleteAsync(Guid id);


    //Task<List<BomTreeDto>> GetBomTreeAsync(BomLookupRequestDto input);
    //Task<List<BomLookupDto>> GetForWorkOrderAsync(BomLookupRequestDto input);
    //Task<List<BomLookupDto>> GetMaterialDetailsAsync(BomLookupRequestDto input);
}