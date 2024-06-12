using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders;

public interface IPurchaseOrderRepository : IRepository<PurchaseOrder, Guid>
{

}
