using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices;

public class PurchasePriceRepository : EfCoreRepository<ERPDbContext, PurchasePrice, Guid>, IPurchasePriceRepository
{
    public PurchasePriceRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<PurchasePrice>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}