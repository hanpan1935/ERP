using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages;

public class PurchaseStorageDetailRepository : EfCoreRepository<ERPDbContext, PurchaseStorageDetail, Guid>, IPurchaseStorageDetailRepository
{
    public PurchaseStorageDetailRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<PurchaseStorageDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}