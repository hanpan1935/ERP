using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms;

public interface IInventoryTransformRepository : IRepository<InventoryTransform, Guid>
{
}
