using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;

public static class WorkOrderOutDetailEfCoreQueryableExtensions
{
    public static IQueryable<WorkOrderOutDetail> IncludeDetails(this IQueryable<WorkOrderOutDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(m => m.WorkOrderOut).ThenInclude(m => m.MaterialApplyDetail).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            .Include(m => m.Location).ThenInclude(m => m.Warehouse)
            .Include(m => m.WorkOrderOut).ThenInclude(m => m.MaterialApplyDetail).ThenInclude(m => m.MaterialApply).ThenInclude(m => m.WorkOrder)
            ;
    }
}
