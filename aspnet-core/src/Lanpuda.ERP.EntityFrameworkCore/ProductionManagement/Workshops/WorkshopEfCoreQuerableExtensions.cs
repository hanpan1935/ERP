using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.Workshops;

public static class WorkshopEfCoreQueryableExtensions
{
    public static IQueryable<Workshop> IncludeDetails(this IQueryable<Workshop> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Creator)
            ;
    }
}
