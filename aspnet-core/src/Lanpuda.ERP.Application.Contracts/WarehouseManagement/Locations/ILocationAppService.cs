using System;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.Locations;

public interface ILocationAppService : IApplicationService
{
    Task<LocationDto> GetAsync(Guid id);

    Task<PagedResultDto<LocationDto>> GetPagedListAsync(LocationPagedRequestDto input);

    Task CreateAsync(LocationCreateDto input);

    Task UpdateAsync(Guid id, LocationUpdateDto input);

    Task DeleteAsync(Guid id);

    Task<List<LocationDto>> GetByWarehouseIdAsync(Guid warehouseId);

}