using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.WarehouseManagement.Inventories;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts
{
    public class WorkOrderOutDetail : AuditedAggregateRoot<Guid>
    {
        [Required]
        public Guid WorkOrderOutId { get; set; }
        public WorkOrderOut WorkOrderOut { get; set; }


        public Guid LocationId { get; set; }
        public Location Location { get; set; }


        [MaxLength(128)]
        public string Batch { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        [Required]
        public double Quantity { get; set; }


        public int Sort { get; set; }

        protected WorkOrderOutDetail()
        {
        }

        public WorkOrderOutDetail(Guid id) : base(id)
        {
        }
    }
}
