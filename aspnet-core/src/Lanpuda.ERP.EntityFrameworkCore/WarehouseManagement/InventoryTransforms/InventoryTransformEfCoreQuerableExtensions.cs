using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms;

public static class InventoryTransformEfCoreQueryableExtensions
{
    public static IQueryable<InventoryTransform> IncludeDetails(this IQueryable<InventoryTransform> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.KeeperUser)
            .Include(x => x.BeforeDetails).ThenInclude(m => m.Location).ThenInclude(m => m.Warehouse)
            .Include(x => x.BeforeDetails).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            .Include(x => x.AfterDetails).ThenInclude(m => m.Location).ThenInclude(m => m.Warehouse)
            .Include(x => x.AfterDetails).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            .Include(x => x.Creator)
            ;
    }
}
