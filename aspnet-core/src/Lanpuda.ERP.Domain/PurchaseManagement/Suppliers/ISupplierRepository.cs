using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.PurchaseManagement.Suppliers;

public interface ISupplierRepository : IRepository<Supplier, Guid>
{
}
