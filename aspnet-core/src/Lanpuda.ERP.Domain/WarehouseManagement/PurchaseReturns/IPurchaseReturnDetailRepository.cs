using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns;

public interface IPurchaseReturnDetailRepository : IRepository<PurchaseReturnDetail, Guid>
{
}
