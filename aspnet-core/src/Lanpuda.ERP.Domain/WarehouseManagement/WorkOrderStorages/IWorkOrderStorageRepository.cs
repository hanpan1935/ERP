using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;

public interface IWorkOrderStorageRepository : IRepository<WorkOrderStorage, Guid>
{
}
