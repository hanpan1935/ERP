using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages;

public interface IPurchaseStorageRepository : IRepository<PurchaseStorage, Guid>
{
}
