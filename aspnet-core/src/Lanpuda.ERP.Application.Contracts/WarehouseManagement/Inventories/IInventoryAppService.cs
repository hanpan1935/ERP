using System;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.Inventories;

public interface IInventoryAppService : IApplicationService
{
    //Task<InventoryDto> GetAsync(Guid id);

    Task<InventoryPagedResultDto> GetPagedListAsync(InventoryPagedRequestDto input);

    //Task<List<InventoryDto>> GetListAsync(InventoryListRequestDto input);
}