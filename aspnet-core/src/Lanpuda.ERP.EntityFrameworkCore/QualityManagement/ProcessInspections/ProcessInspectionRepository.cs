using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.QualityManagement.ProcessInspections;

public class ProcessInspectionRepository : EfCoreRepository<ERPDbContext, ProcessInspection, Guid>, IProcessInspectionRepository
{
    public ProcessInspectionRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<ProcessInspection>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}