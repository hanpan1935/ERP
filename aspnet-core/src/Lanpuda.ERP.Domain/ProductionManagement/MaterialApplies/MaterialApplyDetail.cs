using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies
{
    public class MaterialApplyDetail : AuditedAggregateRoot<Guid>
    {
        public Guid MaterialApplyId { get; set; }
        public MaterialApply MaterialApply { get; set; }


        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public double Quantity { get; set; }

        /// <summary>
        /// 标准用量
        /// </summary>
        public double StandardQuantity { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 1对1 生产领料
        /// </summary>
        public WorkOrderOut WorkOrderOut { get; set; }

        public int Sort { get; set; }
        protected MaterialApplyDetail()
        {
        }

        public MaterialApplyDetail(Guid id) : base(id)
        {
        }
    }
}
