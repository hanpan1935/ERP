using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;

public static class WorkOrderStorageDetailEfCoreQueryableExtensions
{
    public static IQueryable<WorkOrderStorageDetail> IncludeDetails(this IQueryable<WorkOrderStorageDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.WorkOrderStorage).ThenInclude(m=>m.WorkOrderStorageApply).ThenInclude(m=>m.WorkOrder) // TODO: AbpHelper generated
            ;
    }
}
