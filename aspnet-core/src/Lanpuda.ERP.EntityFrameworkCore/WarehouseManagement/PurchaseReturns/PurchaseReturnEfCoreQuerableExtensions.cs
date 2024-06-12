using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns;

public static class PurchaseReturnEfCoreQueryableExtensions
{
    public static IQueryable<PurchaseReturn> IncludeDetails(this IQueryable<PurchaseReturn> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.PurchaseReturnApplyDetail).ThenInclude(m => m.PurchaseStorageDetail).ThenInclude(m => m.PurchaseStorage).ThenInclude(m => m.ArrivalNoticeDetail).ThenInclude(m => m.PurchaseOrderDetail).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            .Include(x => x.PurchaseReturnApplyDetail).ThenInclude(m => m.PurchaseReturnApply).ThenInclude(m => m.Supplier)
            .Include(x => x.Creator)
            .Include(x => x.KeeperUser)
            .Include(m => m.Details).ThenInclude(m=>m.Location).ThenInclude(m=>m.Warehouse)
            ;

        //PurchaseReturnApplyDetail.PurchaseReturnApply
    }
}
