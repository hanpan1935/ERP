using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies;

public static class PurchaseApplyEfCoreQueryableExtensions
{
    public static IQueryable<PurchaseApply> IncludeDetails(this IQueryable<PurchaseApply> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Creator)
            .Include(x => x.Mps)
            .Include(x => x.Details).ThenInclude(m=>m.Product).ThenInclude(m=>m.ProductUnit)
            ;
    }
}
