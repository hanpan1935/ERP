using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.SalesManagement.SalesOrders;

public class SalesOrderRepository : EfCoreRepository<ERPDbContext, SalesOrder, Guid>, ISalesOrderRepository
{
    public SalesOrderRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<SalesOrder>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }

    public async Task<IQueryable<SalesOrder>> WithProfileDetailsAsync()
    {
        var queryable = await GetQueryableAsync();
        //queryable =
        //    queryable
        //    .Include(m => m.Details).ThenInclude(m => m.ShipmentApplyDetails).ThenInclude(m => m.ShipmentApply)
        //    .Include(m => m.Details).ThenInclude(m => m.SalesReturnApplyDetails).ThenInclude(m => m.SalesReturnApply)
        //    .Include(m => m.Details).ThenInclude(m => m.SalesOutDetails).ThenInclude(m => m.SalesOut)
        //    .Include(m => m.Details).ThenInclude(m => m.SalesReturnDetails).ThenInclude(m => m.SalesReturn)
        //    .Include(m => m.Details).ThenInclude(m => m.MpsDetails)
        //    ;
        return queryable;
    }
}