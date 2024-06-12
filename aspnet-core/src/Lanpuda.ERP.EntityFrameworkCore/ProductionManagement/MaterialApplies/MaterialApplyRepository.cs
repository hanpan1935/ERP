using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies;

public class MaterialApplyRepository : EfCoreRepository<ERPDbContext, MaterialApply, Guid>, IMaterialApplyRepository
{
    public MaterialApplyRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<MaterialApply>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}