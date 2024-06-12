using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies;

public class ShipmentApplyRepository : EfCoreRepository<ERPDbContext, ShipmentApply, Guid>, IShipmentApplyRepository
{
    public ShipmentApplyRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<ShipmentApply>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}