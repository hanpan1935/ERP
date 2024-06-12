using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;

public class WorkOrderStorageDetailRepository : EfCoreRepository<ERPDbContext, WorkOrderStorageDetail, Guid>, IWorkOrderStorageDetailRepository
{
    public WorkOrderStorageDetailRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<WorkOrderStorageDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}