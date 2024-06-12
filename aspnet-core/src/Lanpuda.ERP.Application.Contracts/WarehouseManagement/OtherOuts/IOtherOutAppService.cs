using System;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.OtherOuts.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts;

public interface IOtherOutAppService : IApplicationService
{
    Task<OtherOutDto> GetAsync(Guid id);

    Task<PagedResultDto<OtherOutDto>> GetPagedListAsync(OtherOutPagedRequestDto input);

    Task CreateAsync(OtherOutCreateDto input);

    Task UpdateAsync(Guid id, OtherOutUpdateDto input);

    Task DeleteAsync(Guid id);

    Task OutedAsync(Guid id);
}