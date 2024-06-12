using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.SalesManagement.SalesOrders;

public interface ISalesOrderRepository : IRepository<SalesOrder, Guid>
{
    Task<IQueryable<SalesOrder>> WithProfileDetailsAsync();
}
