using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves;

public interface IInventoryMoveDetailRepository : IRepository<InventoryMoveDetail, Guid>
{
}
