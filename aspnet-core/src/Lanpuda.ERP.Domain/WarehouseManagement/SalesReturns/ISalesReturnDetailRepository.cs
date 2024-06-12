using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns;

public interface ISalesReturnDetailRepository : IRepository<SalesReturnDetail, Guid>
{
}
