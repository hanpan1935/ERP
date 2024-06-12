using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns;

public static class SalesReturnDetailEfCoreQueryableExtensions
{
    public static IQueryable<SalesReturnDetail> IncludeDetails(this IQueryable<SalesReturnDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.SalesReturn) // TODO: AbpHelper generated
            ;
    }
}
