using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.SalesManagement.SalesOrders;

public interface ISalesOrderDetailRepository : IRepository<SalesOrderDetail, Guid>
{
}
