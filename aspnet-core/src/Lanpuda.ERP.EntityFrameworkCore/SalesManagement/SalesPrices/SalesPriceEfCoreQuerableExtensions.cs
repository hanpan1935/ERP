using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.SalesPrices;

public static class SalesPriceEfCoreQueryableExtensions
{
    public static IQueryable<SalesPrice> IncludeDetails(this IQueryable<SalesPrice> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
             .Include(x => x.Customer) // TODO: AbpHelper generated
             .Include(x => x.Details.OrderBy(m=>m.Sort)).ThenInclude(m => m.Product).ThenInclude(x => x.ProductUnit)
             .Include(m => m.Creator)
            ;
    }
}
