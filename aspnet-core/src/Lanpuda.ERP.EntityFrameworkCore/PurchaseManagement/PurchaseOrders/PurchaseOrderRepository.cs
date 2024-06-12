using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders;

public class PurchaseOrderRepository : EfCoreRepository<ERPDbContext, PurchaseOrder, Guid>, IPurchaseOrderRepository
{
    public PurchaseOrderRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<PurchaseOrder>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}