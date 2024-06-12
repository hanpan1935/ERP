using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses;

public interface IWarehouseRepository : IRepository<Warehouse, Guid>
{
}
