using System;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;
using Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages;

public interface IPurchaseStorageAppService : IApplicationService
{
    Task<PurchaseStorageDto> GetAsync(Guid id);

    Task<PagedResultDto<PurchaseStorageDto>> GetPagedListAsync(PurchaseStoragePagedRequestDto input);

    Task UpdateAsync(Guid id, PurchaseStorageUpdateDto input);

    Task StoragedAsync(Guid id);

    Task<PagedResultDto<PurchaseStorageDetailSelectDto>> GetDetailPagedListAsync(PurchaseStorageDetailPagedRequestDto input);
}