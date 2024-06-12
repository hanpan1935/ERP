using System;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;

public interface IWorkOrderReturnAppService : IApplicationService
{
    Task<WorkOrderReturnDto> GetAsync(Guid id);
    Task<PagedResultDto<WorkOrderReturnDto>> GetPagedListAsync(WorkOrderReturnPagedRequestDto input);
    Task UpdateAsync(Guid id, WorkOrderReturnUpdateDto input);
    Task StoragedAsync(Guid id);
}

