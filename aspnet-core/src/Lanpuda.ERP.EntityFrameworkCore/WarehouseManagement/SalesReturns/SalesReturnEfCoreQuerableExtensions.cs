using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns;

public static class SalesReturnEfCoreQueryableExtensions
{
    public static IQueryable<SalesReturn> IncludeDetails(this IQueryable<SalesReturn> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Creator)
            .Include(x => x.SalesReturnApplyDetail).ThenInclude(m=>m.SalesOutDetail).ThenInclude(m => m.SalesOut)
            .ThenInclude(m => m.ShipmentApplyDetail).ThenInclude(m => m.SalesOrderDetail)
            .ThenInclude(m=>m.Product).ThenInclude(m=>m.ProductUnit)
            .Include(x => x.SalesReturnApplyDetail).ThenInclude(m => m.SalesOutDetail).ThenInclude(m => m.SalesOut)
            .ThenInclude(m => m.ShipmentApplyDetail).ThenInclude(m => m.SalesOrderDetail)
            .ThenInclude(m => m.Product).ThenInclude(m => m.DefaultLocation)
            .Include(x => x.SalesReturnApplyDetail).ThenInclude(m => m.SalesReturnApply).ThenInclude(m=>m.Customer)
            .Include(m=>m.Details).ThenInclude(m=>m.Location).ThenInclude(m=>m.Warehouse)
            ;
    }
}
