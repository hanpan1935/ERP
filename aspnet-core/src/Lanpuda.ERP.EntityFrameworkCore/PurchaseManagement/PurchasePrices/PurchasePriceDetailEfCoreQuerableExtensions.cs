using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices;

public static class PurchasePriceDetailEfCoreQueryableExtensions
{
    public static IQueryable<PurchasePriceDetail> IncludeDetails(this IQueryable<PurchasePriceDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Product) // TODO: AbpHelper generated
            .Include(x => x.PurchasePrice)
            ;
    }
}
