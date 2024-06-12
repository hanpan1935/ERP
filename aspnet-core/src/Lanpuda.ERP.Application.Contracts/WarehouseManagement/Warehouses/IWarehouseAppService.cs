using System;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses;

public interface IWarehouseAppService :IApplicationService
{
    Task<WarehouseDto> GetAsync(Guid id);

    Task<PagedResultDto<WarehouseDto>> GetPagedListAsync(WarehousePagedRequestDto input);

    Task CreateAsync(WarehouseCreateDto input);

    Task UpdateAsync(Guid id, WarehouseUpdateDto input);

    Task DeleteAsync(Guid id);

    Task<List<WarehouseDto>> GetAllAsync();
}