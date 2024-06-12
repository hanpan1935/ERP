using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts;

public static class OtherOutEfCoreQueryableExtensions
{
    public static IQueryable<OtherOut> IncludeDetails(this IQueryable<OtherOut> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Details).ThenInclude(m => m.Location).ThenInclude(m => m.Warehouse)
            .Include(x => x.Details).ThenInclude(m => m.Product).ThenInclude(m=>m.ProductUnit)
            .Include(x => x.KeeperUser)
            .Include(x => x.Creator)
            ;
    }
}
