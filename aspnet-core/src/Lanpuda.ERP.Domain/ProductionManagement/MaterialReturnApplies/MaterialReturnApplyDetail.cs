using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.ProductionManagement.MaterialApplies;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies
{
    public class MaterialReturnApplyDetail : AuditedAggregateRoot<Guid>
    {
        public Guid MaterialReturnApplyId { get; set; }
        public MaterialReturnApply MaterialReturnApply { get; set; }


        public Guid WorkOrderOutDetailId { get; set; }
        public WorkOrderOutDetail WorkOrderOutDetail { get; set; }


        /// <summary>
        /// �������� 1��1
        /// </summary>
        public WorkOrderReturn WorkOrderReturn { get; set; }


        /// <summary>
        /// ��������
        /// </summary>
        public double Quantity { get; set; }


        public int Sort { get; set; }


        protected MaterialReturnApplyDetail()
        {
        }

        public MaterialReturnApplyDetail(Guid id) : base(id)
        {
        }
    }
}
