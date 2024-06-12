using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages;

public static class PurchaseStorageDetailEfCoreQueryableExtensions
{
    public static IQueryable<PurchaseStorageDetail> IncludeDetails(this IQueryable<PurchaseStorageDetail> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.PurchaseStorage).ThenInclude(m => m.ArrivalNoticeDetail).ThenInclude(m => m.PurchaseOrderDetail).ThenInclude(m => m.Product).ThenInclude(m => m.ProductUnit)
            .Include(x => x.PurchaseStorage).ThenInclude(m => m.ArrivalNoticeDetail).ThenInclude(m => m.PurchaseOrderDetail).ThenInclude(m => m.PurchaseOrder).ThenInclude(m => m.Supplier)
            .Include(x => x.Location).ThenInclude(m => m.Warehouse)
            ;
    }
}
