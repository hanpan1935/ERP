using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.Boms;

public static class BomDetailEfCoreQueryableExtensions
{
    public static IQueryable<BomDetail> IncludeDetails(this IQueryable<BomDetail> queryable, bool include = true)
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
