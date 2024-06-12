using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies;

public static class ShipmentApplyDetailEfCoreQueryableExtensions
{
    public static IQueryable<ShipmentApplyDetail> IncludeDetails(this IQueryable<ShipmentApplyDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.ShipmentApply).ThenInclude(m=>m.Customer) // TODO: AbpHelper generated
            ;
    }
}
