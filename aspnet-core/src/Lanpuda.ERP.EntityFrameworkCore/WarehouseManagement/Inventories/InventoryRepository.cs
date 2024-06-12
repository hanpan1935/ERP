using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Lanpuda.ERP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.ERP.WarehouseManagement.Inventories;

public class InventoryRepository : EfCoreRepository<ERPDbContext, Inventory, Guid>, IInventoryRepository
{
    public InventoryRepository(IDbContextProvider<ERPDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<Inventory> FindExistingAsync(Guid productId, Guid locationId, string batch)
    {
        var queryable = await GetQueryableAsync();
        Inventory inventory = queryable.Where(m => m.ProductId == productId && m.LocationId == locationId && m.Batch == batch).FirstOrDefault();
        return inventory;
    }

    public async Task<double> GetSumQuantityByProductIdAsync(Guid productId)
    {
        var queryable = await GetQueryableAsync();

        queryable = queryable.Where(m => m.ProductId == productId);

        double res = await queryable.SumAsync(m => m.Quantity);

        return res;

    }

    public override async Task<IQueryable<Inventory>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }

    /// <summary>
    /// 入库
    /// </summary>
    /// <param name="locationId"></param>
    /// <param name="productId"></param>
    /// <param name="quantity"></param>
    /// <param name="batch"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public async Task<Inventory> StorageAsync(Guid locationId, Guid productId, double quantity, string batch, double? price = null)
    {
        if (quantity <= 0)
        {
            throw new UserFriendlyException("数量必须大于0");
        }
        var dbset = await GetDbSetAsync();
        var queryable = await GetQueryableAsync();
        Inventory inventory = queryable.Where(m => m.ProductId == productId && m.LocationId == locationId && m.Batch == batch).FirstOrDefault();
        if (inventory != null)
        {
            inventory.Quantity = inventory.Quantity + quantity;
            dbset.Update(inventory);
            return inventory;
        }
        else
        {
            Inventory newInventory = new Inventory(GuidGenerator.Create());
            newInventory.LocationId = locationId;
            newInventory.ProductId = productId;
            newInventory.Quantity = quantity;
            newInventory.Batch = batch;
            newInventory.Price = price;
            await dbset.AddAsync(newInventory);
            return newInventory;
        }
    }

    /// <summary>
    /// 出库
    /// </summary>
    /// <param name="locationId">库位id</param>
    /// <param name="productId">产品id</param>
    /// <param name="quantity">入库数量</param>
    /// <param name="batch">入库批次</param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task<double> OutAsync(Guid locationId, Guid productId, double quantity, string batch)
    {
        if (quantity <= 0) { throw new UserFriendlyException("数量必须大于0"); }
        var queryable = await WithDetailsAsync();
        var dbSet = await GetDbSetAsync();
        Inventory inventory = queryable.Where(m => m.LocationId == locationId && m.ProductId == productId && m.Batch == batch).FirstOrDefault();
        if (inventory == null)
        {
            throw new UserFriendlyException("库存不存在");
        }
        //减库存
        inventory.Quantity = inventory.Quantity - quantity;
        if (inventory.Quantity < 0) 
        {
            throw new UserFriendlyException( inventory.Product.Name +  "库存不足");
        }
        else if (inventory.Quantity == 0) //出库后库存为零删除这条记录
        {
            dbSet.Remove(inventory);
            return 0;
        }
        else
        {
            dbSet.Update(inventory);
            return inventory.Quantity;
        }
    }

}