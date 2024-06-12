using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.BasicData.ProductCategories;

public class ProductCategoryRepository : EfCoreRepository<ERPDbContext, ProductCategory, Guid>, IProductCategoryRepository
{
    public ProductCategoryRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<ProductCategory>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}