using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.QualityManagement.ArrivalInspections;

public class ArrivalInspectionRepository : EfCoreRepository<ERPDbContext, ArrivalInspection, Guid>, IArrivalInspectionRepository
{
    public ArrivalInspectionRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<ArrivalInspection>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}