using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks;

public class InventoryCheckRepository : EfCoreRepository<ERPDbContext, InventoryCheck, Guid>, IInventoryCheckRepository
{
    public InventoryCheckRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<InventoryCheck>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}