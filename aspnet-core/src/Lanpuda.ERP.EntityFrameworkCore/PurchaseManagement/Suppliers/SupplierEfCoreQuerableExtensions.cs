using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.Suppliers;

public static class SupplierEfCoreQueryableExtensions
{
    public static IQueryable<Supplier> IncludeDetails(this IQueryable<Supplier> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable.Include(x => x.Creator);
    }
}
