using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices;

public static class ArrivalNoticeEfCoreQueryableExtensions
{
    public static IQueryable<ArrivalNotice> IncludeDetails(this IQueryable<ArrivalNotice> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
             .Include(m => m.Creator)
             .Include(x => x.Details.OrderBy(m => m.Sort)).ThenInclude(m => m.PurchaseOrderDetail).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
             .Include(x => x.Details.OrderBy(m => m.Sort)).ThenInclude(m => m.PurchaseOrderDetail).ThenInclude(m => m.PurchaseOrder)
            ;
    }
}
