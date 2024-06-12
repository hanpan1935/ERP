using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos.Profiles;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.SalesManagement.SalesOrders;

public interface ISalesOrderAppService : IApplicationService
{
    Task<SalesOrderGetDto> GetAsync(Guid id);

    Task<PagedResultDto<SalesOrderDto>> GetPagedListAsync(SalesOrderPagedRequestDto input);

    Task CreateAsync(SalesOrderCreateDto input);

    Task UpdateAsync(Guid id, SalesOrderUpdateDto input);

    Task DeleteAsync(Guid id);

    Task ConfirmAsync(Guid id);

    Task CloseOrderAsync(Guid id);

    //Task<SalesOrderProfileDto> GetProfileAsync(Guid id);

    Task<PagedResultDto<SalesOrderDetailSelectDto>> GetDetailPagedListAsync(SalesOrderDetailPagedRequestDto input);

    Task CreateMpsAsync(Guid id);

}