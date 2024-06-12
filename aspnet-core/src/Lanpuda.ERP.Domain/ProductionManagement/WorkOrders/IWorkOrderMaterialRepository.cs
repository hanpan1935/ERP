using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders;

public interface IWorkOrderMaterialRepository : IRepository<WorkOrderMaterial, Guid>
{
}
