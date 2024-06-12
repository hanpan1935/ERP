using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts;

public static class SalesOutDetailEfCoreQueryableExtensions
{
    public static IQueryable<SalesOutDetail> IncludeDetails(this IQueryable<SalesOutDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.SalesOut) .ThenInclude(m=>m.ShipmentApplyDetail).ThenInclude(m=>m.ShipmentApply).ThenInclude(m=>m.Customer)
            .Include(x => x.SalesOut).ThenInclude(m => m.ShipmentApplyDetail).ThenInclude(m=>m.SalesOrderDetail).ThenInclude(x => x.Product).ThenInclude(m=>m.ProductUnit)
            ;
    }
}
