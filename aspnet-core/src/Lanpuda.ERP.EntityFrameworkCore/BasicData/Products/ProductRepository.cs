using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.BasicData.Products;

public class ProductRepository : EfCoreRepository<ERPDbContext, Product, Guid>, IProductRepository
{
    public ProductRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Product>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}