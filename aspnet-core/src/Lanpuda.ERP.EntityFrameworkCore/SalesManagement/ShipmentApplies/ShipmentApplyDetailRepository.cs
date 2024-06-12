using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies;

public class ShipmentApplyDetailRepository : EfCoreRepository<ERPDbContext, ShipmentApplyDetail, Guid>, IShipmentApplyDetailRepository
{
    public ShipmentApplyDetailRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<ShipmentApplyDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}