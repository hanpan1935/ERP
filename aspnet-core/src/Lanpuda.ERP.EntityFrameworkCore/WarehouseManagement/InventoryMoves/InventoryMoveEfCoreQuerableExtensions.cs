using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves;

public static class InventoryMoveEfCoreQueryableExtensions
{
    public static IQueryable<InventoryMove> IncludeDetails(this IQueryable<InventoryMove> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.KeeperUser)
            .Include(x => x.Details).ThenInclude(x => x.Product).ThenInclude(m=>m.ProductUnit)
            .Include(x => x.Details).ThenInclude(x => x.OutLocation).ThenInclude(m => m.Warehouse)
            .Include(x => x.Details).ThenInclude(x => x.InLocation).ThenInclude(m => m.Warehouse)
            .Include(x => x.Creator)
            ;
    }
}
