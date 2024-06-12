using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.QualityManagement.ProcessInspections;

public static class ProcessInspectionEfCoreQueryableExtensions
{
    public static IQueryable<ProcessInspection> IncludeDetails(this IQueryable<ProcessInspection> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.WorkOrderStorageApply).ThenInclude(m => m.WorkOrder).ThenInclude(m=>m.Product).ThenInclude(m=>m.ProductUnit) 
            .Include(x => x.Creator)
            .Include(x => x.ConfirmeUser)
            ;
    }
}
