using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;

public static class MaterialReturnApplyEfCoreQueryableExtensions
{
    public static IQueryable<MaterialReturnApply> IncludeDetails(this IQueryable<MaterialReturnApply> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(m => m.Creator)
            .Include(x => x.Details.OrderBy(m => m.Sort)).ThenInclude(m => m.WorkOrderOutDetail).ThenInclude(m => m.WorkOrderOut).ThenInclude(m => m.MaterialApplyDetail).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            ;
    }
}
