using System;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Collections.Generic;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders;

public interface IPurchaseOrderAppService : IApplicationService
{
    Task<PurchaseOrderDto> GetAsync(Guid id);

    Task<PagedResultDto<PurchaseOrderDto>> GetPagedListAsync(PurchaseOrderPagedRequestDto input);

    Task CreateAsync(PurchaseOrderCreateDto input);
         
    Task UpdateAsync(Guid id, PurchaseOrderUpdateDto input);

    Task DeleteAsync(Guid id);

    Task CloseAsync(Guid id);

    Task ConfirmeAsync(Guid id);

    Task<PagedResultDto<PurchaseOrderDetailSelectDto>> GetDetailPagedListAsync(PurchaseOrderDetailPagedRequestDto input);
}