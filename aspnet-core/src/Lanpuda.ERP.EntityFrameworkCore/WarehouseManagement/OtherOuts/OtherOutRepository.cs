using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts;

public class OtherOutRepository : EfCoreRepository<ERPDbContext, OtherOut, Guid>, IOtherOutRepository
{
    public OtherOutRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<OtherOut>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}