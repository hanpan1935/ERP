using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;

public interface IWorkOrderReturnRepository : IRepository<WorkOrderReturn, Guid>
{
}
