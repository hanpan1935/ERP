using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages;

public static class OtherStorageDetailEfCoreQueryableExtensions
{
    public static IQueryable<OtherStorageDetail> IncludeDetails(this IQueryable<OtherStorageDetail> queryable, bool include = true)
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
