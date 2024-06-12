using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.Locations;

public class LocationRepository : EfCoreRepository<ERPDbContext, Location, Guid>, ILocationRepository
{
    public LocationRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Location>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}