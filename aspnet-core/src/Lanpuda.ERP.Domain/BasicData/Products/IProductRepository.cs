using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.BasicData.Products;

public interface IProductRepository : IRepository<Product, Guid>
{
}
