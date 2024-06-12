using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;

public class WorkOrderOutRepository : EfCoreRepository<ERPDbContext, WorkOrderOut, Guid>, IWorkOrderOutRepository
{
    public WorkOrderOutRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<WorkOrderOut>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}