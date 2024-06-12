using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies;

public class MaterialApplyDetailRepository : EfCoreRepository<ERPDbContext, MaterialApplyDetail, Guid>, IMaterialApplyDetailRepository
{
    public MaterialApplyDetailRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<MaterialApplyDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}