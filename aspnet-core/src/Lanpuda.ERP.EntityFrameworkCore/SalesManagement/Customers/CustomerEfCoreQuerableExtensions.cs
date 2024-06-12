using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.Customers;

public static class CustomerEfCoreQueryableExtensions
{
    public static IQueryable<Customer> IncludeDetails(this IQueryable<Customer> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Creator)
            ;
    }
}
