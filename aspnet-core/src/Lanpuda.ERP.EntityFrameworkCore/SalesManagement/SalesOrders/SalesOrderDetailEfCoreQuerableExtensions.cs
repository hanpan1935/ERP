using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.SalesOrders;

public static class SalesOrderDetailEfCoreQueryableExtensions
{
    public static IQueryable<SalesOrderDetail> IncludeDetails(this IQueryable<SalesOrderDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
             .Include(x => x.SalesOrder) // TODO: AbpHelper generated
             .Include(x => x.Product).ThenInclude(m=>m.ProductUnit)
            ;
    }
}
