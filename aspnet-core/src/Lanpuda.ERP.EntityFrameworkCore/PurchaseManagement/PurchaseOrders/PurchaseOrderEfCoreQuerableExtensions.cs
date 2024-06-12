using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders;

public static class PurchaseOrderEfCoreQueryableExtensions
{
    public static IQueryable<PurchaseOrder> IncludeDetails(this IQueryable<PurchaseOrder> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Details.OrderBy(m => m.Sort)).ThenInclude(m=>m.Product).ThenInclude(m=>m.ProductUnit)
            .Include(x => x.ConfirmeUser)
            .Include(x => x.Supplier)
            .Include(x => x.Creator)
            ;
    }
}
