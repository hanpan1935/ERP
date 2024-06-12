using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.Mpses;

public static class MpsEfCoreQueryableExtensions
{
    public static IQueryable<Mps> IncludeDetails(this IQueryable<Mps> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
               .Include(m => m.Product).ThenInclude(m => m.ProductUnit)
               .Include(m => m.Creator)
               .Include(m => m.ConfirmedUser)
               .Include(m => m.Details.OrderBy(m=>m.CreationTime))
               .Include(m => m.MrpDetails.OrderBy(m => m.ProductId).ThenBy(m=>m.RequiredDate)).ThenInclude(m=>m.Product).ThenInclude(m => m.ProductUnit)
               .Include(m => m.WorkOrderDetails.OrderBy(m => m.StartDate)).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
               .Include(m => m.WorkOrderDetails.OrderBy(m => m.StartDate)).ThenInclude(m => m.Workshop)
               .Include(m => m.FinalInspection)
               .Include(m => m.PurchaseApply).ThenInclude(m => m.Details.OrderBy(m=>m.Sort)).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            ;
    }
}
