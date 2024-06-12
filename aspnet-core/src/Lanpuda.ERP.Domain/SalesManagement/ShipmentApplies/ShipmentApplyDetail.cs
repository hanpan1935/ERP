using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.ERP.WarehouseManagement.SalesOuts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies
{
    public class ShipmentApplyDetail : AuditedAggregateRoot<Guid>
    {
        public Guid ShipmentApplyId { get; set; }
        public ShipmentApply ShipmentApply { get; set; }


        public Guid SalesOrderDetailId { get; set; }
        public SalesOrderDetail SalesOrderDetail { get; set; }


        public SalesOut SalesOut { get; set; }

        /// <summary>
        /// 申请数量
        /// </summary>
        public double Quantity { get; set; }


        public int Sort { get; set; }


        protected ShipmentApplyDetail()
        {
        }

        public ShipmentApplyDetail(Guid id) : base(id)
        {
        }
    }
}
