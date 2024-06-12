using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;

public class PurchaseReturnApplyRepository : EfCoreRepository<ERPDbContext, PurchaseReturnApply, Guid>, IPurchaseReturnApplyRepository
{
    public PurchaseReturnApplyRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<PurchaseReturnApply>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}