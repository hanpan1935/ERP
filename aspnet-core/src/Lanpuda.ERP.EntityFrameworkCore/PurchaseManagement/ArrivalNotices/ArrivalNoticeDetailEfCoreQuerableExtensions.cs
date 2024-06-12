using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices;

public static class ArrivalNoticeDetailEfCoreQueryableExtensions
{
    public static IQueryable<ArrivalNoticeDetail> IncludeDetails(this IQueryable<ArrivalNoticeDetail> queryable, bool include = true)
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
