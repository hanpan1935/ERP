using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders;

public interface IWorkOrderRepository : IRepository<WorkOrder, Guid>
{
}
