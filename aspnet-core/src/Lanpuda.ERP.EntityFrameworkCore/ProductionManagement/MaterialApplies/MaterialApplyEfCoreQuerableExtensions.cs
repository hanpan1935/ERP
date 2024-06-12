using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies;

public static class MaterialApplyEfCoreQueryableExtensions
{
    public static IQueryable<MaterialApply> IncludeDetails(this IQueryable<MaterialApply> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(m => m.WorkOrder).ThenInclude(m => m.Mps)
            .Include(m => m.WorkOrder).ThenInclude(m => m.Product)
            .Include(x => x.Details.OrderBy(m => m.Sort)).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            .Include(x => x.Creator)
            ;
    }
}
