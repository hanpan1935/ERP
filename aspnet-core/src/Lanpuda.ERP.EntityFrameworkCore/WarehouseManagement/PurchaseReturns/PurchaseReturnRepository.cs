using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns;

public class PurchaseReturnRepository : EfCoreRepository<ERPDbContext, PurchaseReturn, Guid>, IPurchaseReturnRepository
{
    public PurchaseReturnRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<PurchaseReturn>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}