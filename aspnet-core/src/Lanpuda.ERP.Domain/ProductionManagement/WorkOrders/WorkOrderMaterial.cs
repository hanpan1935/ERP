using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders
{
    /// <summary>
    /// 工单BOM材料
    /// </summary>
    public class WorkOrderMaterial : AuditedAggregateRoot<Guid>
    {
        public Guid WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }


        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public double BomQuantity { get; set; }


        /// <summary>
        /// 应当使用量
        /// Bom数量乘以工单数量
        /// </summary>
        public double Quantity { get; set; }

        public int Sort { get; set; }
        protected WorkOrderMaterial()
        {
        }

        public WorkOrderMaterial(Guid id) : base(id)
        {
            
        }
    }
}
