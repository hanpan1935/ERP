using System;
using System.Threading.Tasks;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies.Dtos;
using Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies;

public interface ISalesReturnApplyAppService :IApplicationService
{
    Task<SalesReturnApplyDto> GetAsync(Guid id);

    Task<PagedResultDto<SalesReturnApplyDto>> GetPagedListAsync(SalesReturnApplyPagedRequestDto input);

    Task CreateAsync(SalesReturnApplyCreateDto input);

    Task UpdateAsync(Guid id, SalesReturnApplyUpdateDto input);

    Task DeleteAsync(Guid id);

    Task ConfirmAsync(Guid id);

}