using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.BasicData.ProductUnits;

public class ProductUnitRepository : EfCoreRepository<ERPDbContext, ProductUnit, Guid>, IProductUnitRepository
{
    public ProductUnitRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<ProductUnit>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}