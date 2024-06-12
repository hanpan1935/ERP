using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.BasicData.ProductUnits;

public interface IProductUnitRepository : IRepository<ProductUnit, Guid>
{
}
