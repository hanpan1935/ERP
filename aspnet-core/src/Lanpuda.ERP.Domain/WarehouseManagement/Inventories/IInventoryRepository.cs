using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.Inventories;

public interface IInventoryRepository : IRepository<Inventory, Guid>
{
    /// <summary>
    /// ������ͬ�Ŀ�棬���ڷ���Inventory�������ڷ���null 
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="locationId"></param>
    /// <param name="batch"></param>
    /// <returns></returns>
    Task<Inventory> FindExistingAsync(Guid productId,Guid locationId,string batch);

    Task<double> GetSumQuantityByProductIdAsync(Guid productId);

    Task<Inventory> StorageAsync(Guid locationId, Guid productId, double quantity, string batch , double? price = null);

    Task<double> OutAsync(Guid locationId, Guid productId, double quantity, string batch);
}
