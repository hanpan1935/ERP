using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses;

public class WarehouseRepository : EfCoreRepository<ERPDbContext, Warehouse, Guid>, IWarehouseRepository
{
    public WarehouseRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Warehouse>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}