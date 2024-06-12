using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns;

public interface IPurchaseReturnAppService : IApplicationService
{
    Task<PurchaseReturnDto> GetAsync(Guid id);

    Task<PagedResultDto<PurchaseReturnDto>> GetPagedListAsync(PurchaseReturnPagedRequestDto input);

    Task UpdateAsync(Guid id, PurchaseReturnUpdateDto input);

    Task OutedAsync(Guid id);

    Task<List<PurchaseReturnDetailDto>> AutoOutAsync(Guid id);
}