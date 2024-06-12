using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;

public static class PurchaseReturnApplyEfCoreQueryableExtensions
{
    public static IQueryable<PurchaseReturnApply> IncludeDetails(this IQueryable<PurchaseReturnApply> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Creator)
            .Include(x => x.ConfirmeUser)
            .Include(x => x.Supplier)
            .Include(x => x.Details.OrderBy(m => m.Sort))
            .ThenInclude(m => m.PurchaseStorageDetail)
            .ThenInclude(m=>m.PurchaseStorage)
            .ThenInclude(m => m.ArrivalNoticeDetail)
            .ThenInclude(m => m.PurchaseOrderDetail)
            .ThenInclude(m => m.Product)
            .ThenInclude(m => m.ProductUnit)
            ;
    }
}
