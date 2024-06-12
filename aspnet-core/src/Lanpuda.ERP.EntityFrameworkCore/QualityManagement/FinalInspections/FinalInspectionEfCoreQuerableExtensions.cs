using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.QualityManagement.FinalInspections;

public static class FinalInspectionEfCoreQueryableExtensions
{
    public static IQueryable<FinalInspection> IncludeDetails(this IQueryable<FinalInspection> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Mps).ThenInclude(m=>m.Product).ThenInclude(m=>m.ProductUnit)
            .Include(x => x.Creator)
            .Include(x => x.ConfirmeUser)
            ;
    }
}
