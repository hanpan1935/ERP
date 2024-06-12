using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.Workshops;

public class WorkshopRepository : EfCoreRepository<ERPDbContext, Workshop, Guid>, IWorkshopRepository
{
    public WorkshopRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }

    public override async Task<IQueryable<Workshop>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}