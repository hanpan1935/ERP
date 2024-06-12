using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts;

public class SalesOutRepository : EfCoreRepository<ERPDbContext, SalesOut, Guid>, ISalesOutRepository
{
    public SalesOutRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    

    public override async Task<IQueryable<SalesOut>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}