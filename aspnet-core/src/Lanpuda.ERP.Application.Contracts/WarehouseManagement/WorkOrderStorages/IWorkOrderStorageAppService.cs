using System;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;

public interface IWorkOrderStorageAppService : IApplicationService
{
    Task<WorkOrderStorageDto> GetAsync(Guid id);

    Task<PagedResultDto<WorkOrderStorageDto>> GetPagedListAsync(WorkOrderStoragePagedRequestDto input);

    Task UpdateAsync(Guid id, WorkOrderStorageUpdateDto input);

    Task StoragedAsync(Guid id);
}