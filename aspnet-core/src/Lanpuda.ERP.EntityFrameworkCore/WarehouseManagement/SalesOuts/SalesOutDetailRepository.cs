using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts;

public class SalesOutDetailRepository : EfCoreRepository<ERPDbContext, SalesOutDetail, Guid>, ISalesOutDetailRepository
{
    public SalesOutDetailRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    //public async Task<double> GetShippedQuantityBySalesOrderDetailIdAsync(Guid productId)
    //{
    //    var queryable = await GetQueryableAsync();
    //    queryable = queryable.Where(m => m.ProductId == productId);
    //    var quantity = await queryable.SumAsync(m => m.Quantity);
    //    return quantity;
    //}


    public override async Task<IQueryable<SalesOutDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}