using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories;

public class SafetyInventoryRepository : EfCoreRepository<ERPDbContext, SafetyInventory, Guid>, ISafetyInventoryRepository
{
    public SafetyInventoryRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<SafetyInventory>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}