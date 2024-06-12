using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories;

public interface ISafetyInventoryRepository : IRepository<SafetyInventory, Guid>
{
}
