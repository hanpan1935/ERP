using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;

public static class MaterialReturnApplyDetailEfCoreQueryableExtensions
{
    public static IQueryable<MaterialReturnApplyDetail> IncludeDetails(this IQueryable<MaterialReturnApplyDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.WorkOrderOutDetail).ThenInclude(m => m.WorkOrderOut).ThenInclude(m => m.MaterialApplyDetail).ThenInclude(m => m.Product).ThenInclude(m=>m.ProductUnit) // TODO: AbpHelper generated
            ;
    }
}
