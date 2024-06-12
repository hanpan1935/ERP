using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts;

public interface ISalesOutRepository : IRepository<SalesOut, Guid>
{
  
}
