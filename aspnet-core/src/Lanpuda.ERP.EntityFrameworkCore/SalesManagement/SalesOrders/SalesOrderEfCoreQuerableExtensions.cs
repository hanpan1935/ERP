using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.SalesOrders;

public static class SalesOrderEfCoreQueryableExtensions
{
    public static IQueryable<SalesOrder> IncludeDetails(this IQueryable<SalesOrder> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
             .Include(x => x.Customer) // TODO: AbpHelper generated
             .Include(x => x.Creator)
             .Include(x => x.ConfirmeUser)
             .Include(x => x.Details.OrderBy(m=>m.Sort)).ThenInclude(x => x.Product).ThenInclude(m => m.ProductUnit)
            ;
    }
}
