using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.ProductionManagement.Mpses;

public interface IMpsRepository : IRepository<Mps, Guid>
{
}
