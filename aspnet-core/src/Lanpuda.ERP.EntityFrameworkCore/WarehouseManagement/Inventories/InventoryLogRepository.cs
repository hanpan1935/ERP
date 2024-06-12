using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.Inventories;

public class InventoryLogRepository : EfCoreRepository<ERPDbContext, InventoryLog, Guid>, IInventoryLogRepository
{
    public InventoryLogRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<InventoryLog>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}