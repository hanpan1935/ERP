using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns;

public interface IPurchaseReturnRepository : IRepository<PurchaseReturn, Guid>
{
}
