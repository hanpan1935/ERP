using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.Inventories;

public interface IInventoryLogRepository : IRepository<InventoryLog, Guid>
{
}
