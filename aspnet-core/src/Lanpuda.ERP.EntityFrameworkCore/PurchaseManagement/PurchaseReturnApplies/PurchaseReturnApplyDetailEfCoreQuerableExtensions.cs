using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;

public static class PurchaseReturnApplyDetailEfCoreQueryableExtensions
{
    public static IQueryable<PurchaseReturnApplyDetail> IncludeDetails(this IQueryable<PurchaseReturnApplyDetail> queryable, bool include = true)
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
