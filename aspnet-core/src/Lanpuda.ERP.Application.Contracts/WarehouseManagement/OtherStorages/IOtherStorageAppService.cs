using System;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.OtherStorages.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages;

public interface IOtherStorageAppService : IApplicationService
{
    Task<OtherStorageDto> GetAsync(Guid id);

    Task<PagedResultDto<OtherStorageDto>> GetPagedListAsync(OtherStoragePagedRequestDto input);

    Task CreateAsync(OtherStorageCreateDto input);

    Task UpdateAsync(Guid id, OtherStorageUpdateDto input);

    Task DeleteAsync(Guid id);

    Task StoragedAsync(Guid id);
}