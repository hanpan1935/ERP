using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies;

public class SalesReturnApplyRepository : EfCoreRepository<ERPDbContext, SalesReturnApply, Guid>, ISalesReturnApplyRepository
{
    public SalesReturnApplyRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<SalesReturnApply>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}