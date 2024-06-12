using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders;

public class WorkOrderRepository : EfCoreRepository<ERPDbContext, WorkOrder, Guid>, IWorkOrderRepository
{
    public WorkOrderRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<WorkOrder>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}