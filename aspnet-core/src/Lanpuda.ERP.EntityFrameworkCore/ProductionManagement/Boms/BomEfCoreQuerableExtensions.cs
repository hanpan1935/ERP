using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.Boms;

public static class BomEfCoreQueryableExtensions
{
    public static IQueryable<Bom> IncludeDetails(this IQueryable<Bom> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
             .Include(x => x.Product).ThenInclude(m => m.ProductUnit)
             .Include(m=>m.Details.OrderBy(m=>m.Sort)).ThenInclude(m=>m.Product).ThenInclude(m=>m.ProductUnit)
             .Include(x => x.Creator)
            ;
    }
}
