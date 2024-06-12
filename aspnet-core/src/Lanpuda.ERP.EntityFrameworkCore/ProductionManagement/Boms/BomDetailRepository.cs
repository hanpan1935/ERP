using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.Boms;

public class BomDetailRepository : EfCoreRepository<ERPDbContext, BomDetail, Guid>, IBomDetailRepository
{
    public BomDetailRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<BomDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}