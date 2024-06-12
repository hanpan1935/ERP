using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies;

public interface IWorkOrderStorageApplyRepository : IRepository<WorkOrderStorageApply, Guid>
{
}
