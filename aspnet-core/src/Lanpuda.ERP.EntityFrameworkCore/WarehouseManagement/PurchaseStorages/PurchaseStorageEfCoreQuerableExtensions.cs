using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages;

public static class PurchaseStorageEfCoreQueryableExtensions
{
    public static IQueryable<PurchaseStorage> IncludeDetails(this IQueryable<PurchaseStorage> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.ArrivalNoticeDetail).ThenInclude(m => m.ArrivalNotice)
            .Include(x => x.ArrivalNoticeDetail).ThenInclude(m => m.PurchaseOrderDetail).ThenInclude(m => m.PurchaseOrder).ThenInclude(m => m.Supplier)
            .Include(x => x.ArrivalNoticeDetail).ThenInclude(m => m.PurchaseOrderDetail).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            .Include(x => x.ArrivalNoticeDetail).ThenInclude(m => m.PurchaseOrderDetail).ThenInclude(m => m.Product).ThenInclude(m => m.DefaultLocation)
            .Include(x => x.KeeperUser)
            .Include(x => x.Details).ThenInclude(x => x.Location).ThenInclude(m => m.Warehouse)
            .Include(x => x.Creator)
            ;
    }
}
