using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders;

public static class WorkOrderEfCoreQueryableExtensions
{
    public static IQueryable<WorkOrder> IncludeDetails(this IQueryable<WorkOrder> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(m => m.Product).ThenInclude(m => m.ProductUnit)
            .Include(m => m.Mps)
            .Include(x => x.StandardMaterialDetails.OrderBy(m => m.Sort)).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            .Include(x => x.Creator)
            .Include(m => m.Workshop)
            ;
    }
}
