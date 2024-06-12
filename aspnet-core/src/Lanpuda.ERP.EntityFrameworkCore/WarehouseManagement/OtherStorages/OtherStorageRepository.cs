using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages;

public class OtherStorageRepository : EfCoreRepository<ERPDbContext, OtherStorage, Guid>, IOtherStorageRepository
{
    public OtherStorageRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<OtherStorage>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}