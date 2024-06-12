using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks;

public static class InventoryCheckDetailEfCoreQueryableExtensions
{
    public static IQueryable<InventoryCheckDetail> IncludeDetails(this IQueryable<InventoryCheckDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            // .Include(x => x.xxx) // TODO: AbpHelper generated
            ;
    }
}
