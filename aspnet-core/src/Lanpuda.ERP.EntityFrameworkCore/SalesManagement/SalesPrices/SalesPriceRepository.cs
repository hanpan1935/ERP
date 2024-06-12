using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.SalesPrices;

public class SalesPriceRepository : EfCoreRepository<ERPDbContext, SalesPrice, Guid>, ISalesPriceRepository
{
    public SalesPriceRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<SalesPrice>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}