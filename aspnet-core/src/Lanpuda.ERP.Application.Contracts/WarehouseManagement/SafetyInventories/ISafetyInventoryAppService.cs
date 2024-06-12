using Lanpuda.ERP.WarehouseManagement.SafetyInventories.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories;

public interface ISafetyInventoryAppService :IApplicationService
{
    Task<SafetyInventoryDto> GetAsync(Guid id);

    Task<PagedResultDto<SafetyInventoryDto>> GetPagedListAsync(SafetyInventoryPagedRequestDto input);

    Task CreateAsync(SafetyInventoryCreateDto input);

    Task UpdateAsync(Guid id, SafetyInventoryUpdateDto input);

    Task DeleteAsync(Guid id);

    Task BulkCreateAsync(List<SafetyInventoryCreateDto> inputs);

    //Task<List<SafetyInventoryDto>> GetAllAsync();
}