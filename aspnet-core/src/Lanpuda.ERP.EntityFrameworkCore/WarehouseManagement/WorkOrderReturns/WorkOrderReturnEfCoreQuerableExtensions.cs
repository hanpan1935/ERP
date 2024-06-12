using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;

public static class WorkOrderReturnEfCoreQueryableExtensions
{
    public static IQueryable<WorkOrderReturn> IncludeDetails(this IQueryable<WorkOrderReturn> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
               .Include(x => x.Details.OrderBy(m => m.Sort)).ThenInclude(m => m.Location).ThenInclude(m => m.Warehouse)
               .Include(x => x.KeeperUser)
               .Include(x => x.Creator)
               .Include(x => x.MaterialReturnApplyDetail).ThenInclude(x => x.WorkOrderOutDetail).ThenInclude(m => m.WorkOrderOut).ThenInclude(x => x.MaterialApplyDetail).ThenInclude(x => x.Product).ThenInclude(m => m.ProductUnit)
               .Include(x => x.MaterialReturnApplyDetail).ThenInclude(x => x.WorkOrderOutDetail).ThenInclude(m => m.WorkOrderOut).ThenInclude(x => x.MaterialApplyDetail).ThenInclude(x => x.Product).ThenInclude(m => m.DefaultLocation)
               .Include(x => x.MaterialReturnApplyDetail).ThenInclude(x => x.WorkOrderOutDetail).ThenInclude(m => m.WorkOrderOut).ThenInclude(x => x.MaterialApplyDetail).ThenInclude(x => x.MaterialApply).ThenInclude(x => x.WorkOrder)
               .Include(x => x.MaterialReturnApplyDetail).ThenInclude(m => m.MaterialReturnApply)
               ;
    }
}
