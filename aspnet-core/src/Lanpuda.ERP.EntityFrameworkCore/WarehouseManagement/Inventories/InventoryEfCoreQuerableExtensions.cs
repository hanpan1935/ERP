using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.Inventories;

public static class InventoryEfCoreQueryableExtensions
{
    public static IQueryable<Inventory> IncludeDetails(this IQueryable<Inventory> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
             .Include(x => x.Product) .ThenInclude(m=>m.ProductUnit)// TODO: AbpHelper generated
             .Include(x => x.Location).ThenInclude(m=>m.Warehouse)
            ;
    }
}
