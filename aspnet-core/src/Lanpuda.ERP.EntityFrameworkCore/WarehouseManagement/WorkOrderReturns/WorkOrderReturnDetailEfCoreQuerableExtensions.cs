using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;

public static class WorkOrderReturnDetailEfCoreQueryableExtensions
{
    public static IQueryable<WorkOrderReturnDetail> IncludeDetails(this IQueryable<WorkOrderReturnDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            // .Include(x => x.xxx) // TODO: AbpHelper generated
            ;
    }
}
