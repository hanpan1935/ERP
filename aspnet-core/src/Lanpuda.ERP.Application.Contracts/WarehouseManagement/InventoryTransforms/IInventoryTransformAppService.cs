using System;
using Lanpuda.ERP.WarehouseManagement.InventoryTransforms.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms;

public interface IInventoryTransformAppService : IApplicationService
{
    Task<InventoryTransformDto> GetAsync(Guid id);

    Task<PagedResultDto<InventoryTransformDto>> GetPagedListAsync(InventoryTransformPagedRequestDto input);

    Task CreateAsync(InventoryTransformCreateDto input);

    Task UpdateAsync(Guid id, InventoryTransformUpdateDto input);

    Task DeleteAsync(Guid id);

    Task ConfirmedAsync(Guid id);
}