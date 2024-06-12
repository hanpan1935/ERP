using System;
using Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;

public interface IMaterialReturnApplyAppService : IApplicationService
{
    Task<MaterialReturnApplyDto> GetAsync(Guid id);

    Task<PagedResultDto<MaterialReturnApplyDto>> GetPagedListAsync(MaterialReturnApplyPagedRequestDto input);

    Task CreateAsync(MaterialReturnApplyCreateDto input);

    Task UpdateAsync(Guid id, MaterialReturnApplyUpdateDto input);

    Task DeleteAsync(Guid id);

    Task ConfirmeAsync(Guid id);

}