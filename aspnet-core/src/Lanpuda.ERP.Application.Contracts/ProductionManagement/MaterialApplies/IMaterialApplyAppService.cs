using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies;

public interface IMaterialApplyAppService : IApplicationService
{
    Task<MaterialApplyDto> GetAsync(Guid id);

    Task<PagedResultDto<MaterialApplyDto>> GetPagedListAsync(MaterialApplyPagedRequestDto input);

    Task CreateAsync(MaterialApplyCreateDto input);

    Task UpdateAsync(Guid id, MaterialApplyUpdateDto input);

    Task DeleteAsync(Guid id);

    Task ConfirmeAsync(Guid id);

}