using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders;

public static class PurchaseOrderDetailEfCoreQueryableExtensions
{
    public static IQueryable<PurchaseOrderDetail> IncludeDetails(this IQueryable<PurchaseOrderDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
             .Include(x => x.PurchaseOrder).ThenInclude(m=>m.Supplier)
             .Include(x => x.Product).ThenInclude(m => m.ProductUnit)
            ;
    }
}
