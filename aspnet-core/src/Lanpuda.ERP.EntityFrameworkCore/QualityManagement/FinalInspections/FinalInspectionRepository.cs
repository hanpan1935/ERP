using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.QualityManagement.FinalInspections;

public class FinalInspectionRepository : EfCoreRepository<ERPDbContext, FinalInspection, Guid>, IFinalInspectionRepository
{
    public FinalInspectionRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<FinalInspection>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}