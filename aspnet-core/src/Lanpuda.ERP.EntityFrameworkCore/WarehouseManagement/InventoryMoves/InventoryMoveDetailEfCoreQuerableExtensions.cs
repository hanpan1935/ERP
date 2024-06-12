using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves;

public static class InventoryMoveDetailEfCoreQueryableExtensions
{
    public static IQueryable<InventoryMoveDetail> IncludeDetails(this IQueryable<InventoryMoveDetail> queryable, bool include = true)
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
