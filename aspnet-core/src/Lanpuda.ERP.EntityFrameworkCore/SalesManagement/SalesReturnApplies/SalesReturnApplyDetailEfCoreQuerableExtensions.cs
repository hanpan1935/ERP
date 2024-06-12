using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies;

public static class SalesReturnApplyDetailEfCoreQueryableExtensions
{
    public static IQueryable<SalesReturnApplyDetail> IncludeDetails(this IQueryable<SalesReturnApplyDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.SalesReturnApply) // TODO: AbpHelper generated
            ;
    }
}
