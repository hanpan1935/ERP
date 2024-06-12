using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts;

public interface ISalesOutDetailRepository : IRepository<SalesOutDetail, Guid>
{
    /// <summary>
    /// �������۶�����ϸId��ȡ�ѷ�������
    /// </summary>
    /// <param name="salesOrderDetailId"></param>
    /// <returns></returns>
    //Task<double> GetShippedQuantityBySalesOrderDetailIdAsync(Guid salesOrderDetailId);
}
