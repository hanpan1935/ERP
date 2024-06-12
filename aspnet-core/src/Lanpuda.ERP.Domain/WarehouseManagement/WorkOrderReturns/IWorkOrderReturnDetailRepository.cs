using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;

public interface IWorkOrderReturnDetailRepository : IRepository<WorkOrderReturnDetail, Guid>
{
}
