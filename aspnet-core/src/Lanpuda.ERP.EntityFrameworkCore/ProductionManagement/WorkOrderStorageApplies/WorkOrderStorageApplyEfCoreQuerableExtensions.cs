using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies;

public static class WorkOrderStorageApplyEfCoreQueryableExtensions
{
    public static IQueryable<WorkOrderStorageApply> IncludeDetails(this IQueryable<WorkOrderStorageApply> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.WorkOrder).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            .Include(x => x.WorkOrder).ThenInclude(m => m.Mps)
            .Include(x => x.WorkOrderStorage)
            .Include(x => x.Creator)
            .Include(x => x.ConfirmedUser)
            ;
    }
}
