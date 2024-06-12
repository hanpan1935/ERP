using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies;

public class PurchaseApplyDetailRepository : EfCoreRepository<ERPDbContext, PurchaseApplyDetail, Guid>, IPurchaseApplyDetailRepository
{
    public PurchaseApplyDetailRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<PurchaseApplyDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}