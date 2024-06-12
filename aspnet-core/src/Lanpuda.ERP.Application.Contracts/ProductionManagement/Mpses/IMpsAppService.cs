using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.ProductionManagement.Mpses;

public interface IMpsAppService : IApplicationService
{
    Task<MpsDto> GetAsync(Guid id);

    Task<PagedResultDto<MpsDto>> GetPagedListAsync(MpsPagedRequestDto input);

    Task CreateAsync(MpsCreateDto input);

    Task UpdateAsync(Guid id, MpsUpdateDto input);

    Task DeleteAsync(Guid id);

    Task ConfirmeAsync(Guid id);

    Task CreateMrpAsync(Guid id);


    Task<MpsProfileDto> GetProfileAsync(Guid id);


    Task CreatePurchaseApplyWorkOrderAsync(CreatePurchaseApplyWorkOrderByMrpInput input);

}