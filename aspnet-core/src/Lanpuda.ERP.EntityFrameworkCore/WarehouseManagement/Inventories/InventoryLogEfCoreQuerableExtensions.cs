using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.Inventories;

public static class InventoryLogEfCoreQueryableExtensions
{
    public static IQueryable<InventoryLog> IncludeDetails(this IQueryable<InventoryLog> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Location).ThenInclude(m=>m.Warehouse) // TODO: AbpHelper generated
            .Include(m=>m.Product).ThenInclude(m=>m.ProductUnit)
            .Include(x => x.Creator)
            ;
    }
}
