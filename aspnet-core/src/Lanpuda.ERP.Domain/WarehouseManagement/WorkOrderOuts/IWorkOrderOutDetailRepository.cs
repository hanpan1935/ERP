using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;

public interface IWorkOrderOutDetailRepository : IRepository<WorkOrderOutDetail, Guid>
{
}
