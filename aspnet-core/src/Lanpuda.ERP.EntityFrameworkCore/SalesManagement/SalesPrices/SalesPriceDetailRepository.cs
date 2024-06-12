using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.SalesPrices;

public class SalesPriceDetailRepository : EfCoreRepository<ERPDbContext, SalesPriceDetail, Guid>, ISalesPriceDetailRepository
{
    public SalesPriceDetailRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<SalesPriceDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}