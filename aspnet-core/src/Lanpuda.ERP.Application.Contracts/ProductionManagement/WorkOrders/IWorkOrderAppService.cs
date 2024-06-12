using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders;

public interface IWorkOrderAppService : IApplicationService
{
    Task<WorkOrderDto> GetAsync(Guid id);

    Task<PagedResultDto<WorkOrderDto>> GetPagedListAsync(WorkOrderPagedRequestDto input);

    Task MultipleCreateAsync(WorkOrderMultipleCreateDto input);

    Task UpdateAsync(Guid id, WorkOrderUpdateDto input);

    Task DeleteAsync(Guid id);

    Task ConfirmeAsync(Guid id);

    Task MultipleConfirmeAsync(List<Guid> ids);

}
