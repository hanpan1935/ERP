using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.ProductionManagement.Boms;

public interface IBomDetailRepository : IRepository<BomDetail, Guid>
{
}
