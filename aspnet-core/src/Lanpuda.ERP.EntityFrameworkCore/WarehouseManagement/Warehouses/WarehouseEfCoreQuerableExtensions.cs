using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses;

public static class WarehouseEfCoreQueryableExtensions
{
    public static IQueryable<Warehouse> IncludeDetails(this IQueryable<Warehouse> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Locations) // TODO: AbpHelper generated
            .Include(x => x.Creator)
            ;
    }
}
