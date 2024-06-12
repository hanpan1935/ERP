using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.SalesPrices;

public static class SalesPriceDetailEfCoreQueryableExtensions
{
    public static IQueryable<SalesPriceDetail> IncludeDetails(this IQueryable<SalesPriceDetail> queryable, bool include = true)
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
