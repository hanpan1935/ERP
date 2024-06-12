using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;

public static class WorkOrderStorageEfCoreQueryableExtensions
{
    public static IQueryable<WorkOrderStorage> IncludeDetails(this IQueryable<WorkOrderStorage> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
               .Include(x => x.WorkOrderStorageApply).ThenInclude(m => m.WorkOrder).ThenInclude(m => m.Product).ThenInclude(m=>m.DefaultLocation)
               .Include(x => x.KeeperUser)
               .Include(x => x.Details.OrderBy(m => m.Sort))
               .Include(x => x.Details.OrderBy(m => m.Sort)).ThenInclude(m => m.Location).ThenInclude(m => m.Warehouse)
               .Include(x => x.Creator)
               ;
    }
}
