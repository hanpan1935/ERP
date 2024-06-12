using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;

public class WorkOrderReturnRepository : EfCoreRepository<ERPDbContext, WorkOrderReturn, Guid>, IWorkOrderReturnRepository
{
    public WorkOrderReturnRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<WorkOrderReturn>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}