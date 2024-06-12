using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;

public static class WorkOrderOutEfCoreQueryableExtensions
{
    public static IQueryable<WorkOrderOut> IncludeDetails(this IQueryable<WorkOrderOut> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
               .Include(x => x.MaterialApplyDetail).ThenInclude(x => x.MaterialApply).ThenInclude(m=>m.WorkOrder)
               .Include(x => x.MaterialApplyDetail).ThenInclude(m => m.Product).ThenInclude(m=>m.ProductUnit)
               .Include(x => x.KeeperUser)
               .Include(x => x.Details.OrderBy(m => m.Sort)).ThenInclude(m => m.Location).ThenInclude(m=>m.Warehouse)
               .Include(x => x.Creator)
               ;
    }
}
