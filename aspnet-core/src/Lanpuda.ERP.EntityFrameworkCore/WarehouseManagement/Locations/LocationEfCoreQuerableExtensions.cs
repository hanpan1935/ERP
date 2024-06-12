using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.Locations;

public static class LocationEfCoreQueryableExtensions
{
    public static IQueryable<Location> IncludeDetails(this IQueryable<Location> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Warehouse)
            .Include(x => x.Creator)
            ;
    }
}
