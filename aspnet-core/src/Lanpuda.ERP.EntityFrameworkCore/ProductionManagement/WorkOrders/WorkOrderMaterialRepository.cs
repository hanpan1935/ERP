using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders;

public class WorkOrderMaterialRepository : EfCoreRepository<ERPDbContext, WorkOrderMaterial, Guid>, IWorkOrderMaterialRepository
{
    public WorkOrderMaterialRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<WorkOrderMaterial>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}