using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies;

public static class PurchaseApplyDetailEfCoreQueryableExtensions
{
    public static IQueryable<PurchaseApplyDetail> IncludeDetails(this IQueryable<PurchaseApplyDetail> queryable, bool include = true)
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
