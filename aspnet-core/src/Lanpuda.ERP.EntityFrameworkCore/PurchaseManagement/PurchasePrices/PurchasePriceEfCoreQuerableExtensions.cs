using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices;

public static class PurchasePriceEfCoreQueryableExtensions
{
    public static IQueryable<PurchasePrice> IncludeDetails(this IQueryable<PurchasePrice> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
             .Include(x => x.Supplier)
             .Include(x => x.Details.OrderBy(m => m.Sort)).ThenInclude(m=>m.Product).ThenInclude(m=>m.ProductUnit)
             .Include(x => x.Creator)
            ;
    }
}
