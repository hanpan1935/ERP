using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;

public interface IWorkOrderOutAppService : IApplicationService
{
    Task<WorkOrderOutDto> GetAsync(Guid id);

    Task<PagedResultDto<WorkOrderOutDto>> GetPagedListAsync(WorkOrderOutPagedRequestDto input);

    Task UpdateAsync(Guid id, WorkOrderOutUpdateDto input);

    Task OutedAsync(Guid id);

    Task<PagedResultDto<WorkOrderOutDetailSelectDto>> GetDetailPagedListAsync(WorkOrderOutDetailPagedRequestDto input);

    Task<List<WorkOrderOutDetailDto>> AutoOutAsync(Guid id);
}