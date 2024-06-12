using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks;

public interface IInventoryCheckDetailRepository : IRepository<InventoryCheckDetail, Guid>
{
}
