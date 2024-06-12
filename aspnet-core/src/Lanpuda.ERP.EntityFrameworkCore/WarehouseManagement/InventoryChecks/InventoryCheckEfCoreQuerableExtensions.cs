using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks;

public static class InventoryCheckEfCoreQueryableExtensions
{
    public static IQueryable<InventoryCheck> IncludeDetails(this IQueryable<InventoryCheck> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.KeeperUser)
            .Include(x => x.Details).ThenInclude(m=>m.Product).ThenInclude(m=>m.ProductUnit)
            .Include(x => x.Details).ThenInclude(m=>m.Location).ThenInclude(m=>m.Warehouse)
            .Include(x => x.Creator)
            ;
    }
}
