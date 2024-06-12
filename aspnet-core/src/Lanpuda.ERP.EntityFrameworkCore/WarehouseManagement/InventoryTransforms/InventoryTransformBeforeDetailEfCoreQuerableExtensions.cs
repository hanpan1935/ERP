using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms;

public static class InventoryTransformBeforeDetailEfCoreQueryableExtensions
{
    public static IQueryable<InventoryTransformBeforeDetail> IncludeDetails(this IQueryable<InventoryTransformBeforeDetail> queryable, bool include = true)
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
