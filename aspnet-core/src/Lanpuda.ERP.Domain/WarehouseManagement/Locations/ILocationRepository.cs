using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.Locations;

public interface ILocationRepository : IRepository<Location, Guid>
{
}
