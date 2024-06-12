using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies;

public static class SalesReturnApplyEfCoreQueryableExtensions
{
    public static IQueryable<SalesReturnApply> IncludeDetails(this IQueryable<SalesReturnApply> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Customer)
            .Include(x => x.Creator)
            .Include(x => x.ConfirmeUser)
            .Include(x => x.Details.OrderBy(m => m.Sort)).ThenInclude(x => x.SalesOutDetail)
            .ThenInclude(x => x.SalesOut).ThenInclude(x => x.ShipmentApplyDetail).ThenInclude(x => x.SalesOrderDetail)
            .ThenInclude(m=>m.Product).ThenInclude(m=>m.ProductUnit)
            ;
    }
}
