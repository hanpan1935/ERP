using System;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.InventoryMoves.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves;

public interface IInventoryMoveAppService : IApplicationService
{
    Task<InventoryMoveDto> GetAsync(Guid id);

    Task<PagedResultDto<InventoryMoveDto>> GetPagedListAsync(InventoryMovePagedRequestDto input);

    Task CreateAsync(InventoryMoveCreateDto input);

    Task UpdateAsync(Guid id, InventoryMoveUpdateDto input);

    Task DeleteAsync(Guid id);

    Task MovedAsync(Guid id);
}