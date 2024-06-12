using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms;

public static class InventoryTransformAfterDetailEfCoreQueryableExtensions
{
    public static IQueryable<InventoryTransformAfterDetail> IncludeDetails(this IQueryable<InventoryTransformAfterDetail> queryable, bool include = true)
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
