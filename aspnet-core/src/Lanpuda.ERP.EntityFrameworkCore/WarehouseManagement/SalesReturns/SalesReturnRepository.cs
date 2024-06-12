using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns;

public class SalesReturnRepository : EfCoreRepository<ERPDbContext, SalesReturn, Guid>, ISalesReturnRepository
{
    public SalesReturnRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<SalesReturn>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}