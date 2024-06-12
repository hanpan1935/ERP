using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms;

public class InventoryTransformRepository : EfCoreRepository<ERPDbContext, InventoryTransform, Guid>, IInventoryTransformRepository
{
    public InventoryTransformRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<InventoryTransform>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}