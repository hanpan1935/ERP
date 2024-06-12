using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages;

public static class OtherStorageEfCoreQueryableExtensions
{
    public static IQueryable<OtherStorage> IncludeDetails(this IQueryable<OtherStorage> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Details).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            .Include(x => x.Details).ThenInclude(m=>m.Location).ThenInclude(m=>m.Warehouse)
            .Include(x=>x.KeeperUser)
            .Include(x => x.Creator)
            ;
    }
}
