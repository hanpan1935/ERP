using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms;

public class InventoryTransformAfterDetailRepository : EfCoreRepository<ERPDbContext, InventoryTransformAfterDetail, Guid>, IInventoryTransformAfterDetailRepository
{
    public InventoryTransformAfterDetailRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<InventoryTransformAfterDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}