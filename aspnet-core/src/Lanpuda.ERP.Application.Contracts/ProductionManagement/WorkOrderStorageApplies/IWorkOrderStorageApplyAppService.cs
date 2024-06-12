using System;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;
using System.Threading.Tasks;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies;


public interface IWorkOrderStorageApplyAppService : IApplicationService
{
    Task<WorkOrderStorageApplyDto> GetAsync(Guid id);

    Task<PagedResultDto<WorkOrderStorageApplyDto>> GetPagedListAsync(WorkOrderStorageApplyPagedRequestDto input);

    Task CreateAsync(WorkOrderStorageApplyCreateDto input);

    Task UpdateAsync(Guid id, WorkOrderStorageApplyUpdateDto input);

    Task DeleteAsync(Guid id);

    Task ConfirmeAsync(Guid id);
}

