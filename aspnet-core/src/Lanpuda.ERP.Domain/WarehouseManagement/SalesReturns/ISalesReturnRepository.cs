using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns;

public interface ISalesReturnRepository : IRepository<SalesReturn, Guid>
{
}
