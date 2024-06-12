using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;

public class MaterialReturnApplyRepository : EfCoreRepository<ERPDbContext, MaterialReturnApply, Guid>, IMaterialReturnApplyRepository
{
    public MaterialReturnApplyRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<MaterialReturnApply>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}