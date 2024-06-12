using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.PurchaseManagement.Suppliers;

public interface ISupplierAppService : IApplicationService
{
    Task<SupplierDto> GetAsync(Guid id);

    Task<PagedResultDto<SupplierDto>> GetPagedListAsync(SupplierPagedRequestDto input);

    Task CreateAsync(SupplierCreateDto input);

    Task UpdateAsync(Guid id, SupplierUpdateDto input);

    Task DeleteAsync(Guid id);

    Task<List<SupplierDto>> GetAllAsync();
}