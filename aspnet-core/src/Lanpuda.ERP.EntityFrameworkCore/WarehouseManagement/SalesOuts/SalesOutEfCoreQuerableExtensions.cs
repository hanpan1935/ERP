using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts;

public static class SalesOutEfCoreQueryableExtensions
{
    public static IQueryable<SalesOut> IncludeDetails(this IQueryable<SalesOut> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
             .Include(m => m.ShipmentApplyDetail).ThenInclude(x => x.ShipmentApply).ThenInclude(m => m.Customer)
             .Include(x => x.ShipmentApplyDetail).ThenInclude(m => m.SalesOrderDetail).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
             .Include(x => x.KeeperUser)
             .Include(x => x.Details).ThenInclude(m => m.Location).ThenInclude(m => m.Warehouse)
             .Include(x => x.Creator)
            ;
    }

}
