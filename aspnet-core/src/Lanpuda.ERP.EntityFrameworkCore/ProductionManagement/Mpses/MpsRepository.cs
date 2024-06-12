using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.Mpses;

public class MpsRepository : EfCoreRepository<ERPDbContext, Mps, Guid>, IMpsRepository
{
    public MpsRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Mps>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}