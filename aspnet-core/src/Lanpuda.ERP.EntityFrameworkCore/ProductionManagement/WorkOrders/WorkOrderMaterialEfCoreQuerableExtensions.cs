using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders;

public static class WorkOrderMaterialEfCoreQueryableExtensions
{
    public static IQueryable<WorkOrderMaterial> IncludeDetails(this IQueryable<WorkOrderMaterial> queryable, bool include = true)
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
