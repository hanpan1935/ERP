using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts;

public static class OtherOutDetailEfCoreQueryableExtensions
{
    public static IQueryable<OtherOutDetail> IncludeDetails(this IQueryable<OtherOutDetail> queryable, bool include = true)
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
