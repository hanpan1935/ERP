using System;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.InventoryChecks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks;

public interface IInventoryCheckAppService : IApplicationService
{
    Task<InventoryCheckDto> GetAsync(Guid id);

    Task<PagedResultDto<InventoryCheckDto>> GetPagedListAsync(InventoryCheckPagedRequestDto input);

    Task CreateAsync(InventoryCheckCreateDto input);

    Task UpdateAsync(Guid id, InventoryCheckUpdateDto input);

    Task DeleteAsync(Guid id);

    Task ConfirmedAsync(Guid id);
}

