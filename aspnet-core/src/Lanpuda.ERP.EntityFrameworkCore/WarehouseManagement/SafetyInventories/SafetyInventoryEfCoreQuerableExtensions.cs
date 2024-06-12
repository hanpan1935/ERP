using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories;

public static class SafetyInventoryEfCoreQueryableExtensions
{
    public static IQueryable<SafetyInventory> IncludeDetails(this IQueryable<SafetyInventory> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Product).ThenInclude(m => m.ProductUnit)
            .Include(x => x.Creator)
            ;
    }
}
