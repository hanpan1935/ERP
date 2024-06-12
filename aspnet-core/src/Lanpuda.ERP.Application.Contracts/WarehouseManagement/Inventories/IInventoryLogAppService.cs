using System;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.Inventories;

public interface IInventoryLogAppService :IApplicationService
{
    //Task<InventoryLogDto> GetAsync(Guid id);

    Task<PagedResultDto<InventoryLogDto>> GetPagedListAsync(InventoryLogPagedRequestDto input);
}