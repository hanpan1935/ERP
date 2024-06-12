using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.QualityManagement.ArrivalInspections;

public static class ArrivalInspectionEfCoreQueryableExtensions
{
    public static IQueryable<ArrivalInspection> IncludeDetails(this IQueryable<ArrivalInspection> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Creator)
            .Include(x => x.ArrivalNoticeDetail).ThenInclude(m => m.ArrivalNotice)
            .Include(x => x.ArrivalNoticeDetail).ThenInclude(m => m.PurchaseOrderDetail).ThenInclude(m=>m.Product).ThenInclude(m=>m.ProductUnit)
            ;
    }
}
