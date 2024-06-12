using System;
using System.Threading.Tasks;
using Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies;

public interface IShipmentApplyAppService : IApplicationService
{
    Task<ShipmentApplyDto> GetAsync(Guid id);

    Task<PagedResultDto<ShipmentApplyDto>> GetPagedListAsync(ShipmentApplyPagedRequestDto input);

    Task CreateAsync(ShipmentApplyCreateDto input);

    Task UpdateAsync(Guid id, ShipmentApplyUpdateDto input);

    Task DeleteAsync(Guid id);

    Task ConfirmAsync(Guid id);
}