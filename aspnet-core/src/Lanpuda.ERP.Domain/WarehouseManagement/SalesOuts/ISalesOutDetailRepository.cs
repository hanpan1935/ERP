using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts;

public interface ISalesOutDetailRepository : IRepository<SalesOutDetail, Guid>
{
    /// <summary>
    /// 根据销售订单明细Id获取已发货数量
    /// </summary>
    /// <param name="salesOrderDetailId"></param>
    /// <returns></returns>
    //Task<double> GetShippedQuantityBySalesOrderDetailIdAsync(Guid salesOrderDetailId);
}
