using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies;

public interface IShipmentApplyRepository : IRepository<ShipmentApply, Guid>
{
}
