using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.BasicData.ProductUnits;

public static class ProductUnitEfCoreQueryableExtensions
{
    public static IQueryable<ProductUnit> IncludeDetails(this IQueryable<ProductUnit> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Creator) // TODO: AbpHelper generated
            ;
    }
}
