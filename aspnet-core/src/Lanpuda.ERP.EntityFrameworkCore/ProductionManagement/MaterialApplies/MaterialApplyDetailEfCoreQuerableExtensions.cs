using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies;

public static class MaterialApplyDetailEfCoreQueryableExtensions
{
    public static IQueryable<MaterialApplyDetail> IncludeDetails(this IQueryable<MaterialApplyDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Product).ThenInclude(m=>m.ProductUnit) // TODO: AbpHelper generated
            ;
    }
}
