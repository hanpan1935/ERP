using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.BasicData.ProductCategories;

public interface IProductCategoryRepository : IRepository<ProductCategory, Guid>
{
}
