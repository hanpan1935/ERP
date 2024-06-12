using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;

public class WorkOrderStorageRepository : EfCoreRepository<ERPDbContext, WorkOrderStorage, Guid>, IWorkOrderStorageRepository
{
    public WorkOrderStorageRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<WorkOrderStorage>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}