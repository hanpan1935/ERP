using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies;

public static class ShipmentApplyEfCoreQueryableExtensions
{
    public static IQueryable<ShipmentApply> IncludeDetails(this IQueryable<ShipmentApply> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Customer)
            .Include(x => x.Creator)
            .Include(x => x.ConfirmeUser)
            .Include(m => m.Details.OrderBy(m => m.Sort)).ThenInclude(m => m.SalesOrderDetail).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            .Include(m => m.Details.OrderBy(m => m.Sort)).ThenInclude(m => m.SalesOrderDetail).ThenInclude(m=>m.SalesOrder)
            ;
    }
}
