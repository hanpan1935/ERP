using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves;

public interface IInventoryMoveRepository : IRepository<InventoryMove, Guid>
{
}
