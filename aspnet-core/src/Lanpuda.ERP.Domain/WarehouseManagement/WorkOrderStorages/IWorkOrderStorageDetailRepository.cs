using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;

public interface IWorkOrderStorageDetailRepository : IRepository<WorkOrderStorageDetail, Guid>
{
}
