using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;

public class WorkOrderReturnDetailRepository : EfCoreRepository<ERPDbContext, WorkOrderReturnDetail, Guid>, IWorkOrderReturnDetailRepository
{
    public WorkOrderReturnDetailRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<WorkOrderReturnDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}