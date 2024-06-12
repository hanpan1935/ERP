using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.Locations;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns
{
    public class WorkOrderReturnDetail : AuditedAggregateRoot<Guid>
    {
        [Required]
        public Guid WorkOrderReturnId { get; set; }
        public WorkOrderReturn WorkOrderReturn { get; set; }


        public Guid LocationId { get; set; }
        public Location Location { get; set; }


        /// <summary>
        /// 入库数量
        /// </summary>
        [Required]
        public double Quantity { get; set; }


        public int Sort { get; set; }


        protected WorkOrderReturnDetail()
        {
        }

        public WorkOrderReturnDetail(Guid id) : base(id)
        {
        }
    }
}
