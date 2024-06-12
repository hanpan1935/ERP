using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.SalesManagement.Customers;

public interface ICustomerAppService : IApplicationService
{
    Task<CustomerDto> GetAsync(Guid id);

    Task<PagedResultDto<CustomerDto>> GetPagedListAsync(CustomerPagedRequestDto input);

    Task CreateAsync(CustomerCreateDto input);

    Task UpdateAsync(Guid id, CustomerUpdateDto input);

    Task DeleteAsync(Guid id);

    Task<List<CustomerLookupDto>> GetAllAsync();
}