using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns
{
    public class PurchaseReturnDetail : AuditedAggregateRoot<Guid>
    {
        [Required]
        public Guid PurchaseReturnId { get; set; }
        public PurchaseReturn PurchaseReturn { get; set; }

        /// <summary>
        /// 冗余数据
        /// </summary>
        //public Guid ProductId { get; set; }
        //public Product Product { get; set; }


        public Guid LocationId { get; set; }
        public Location Location { get; set; }


        [MaxLength(128)]
        public string Batch { get; set; }

        /// <summary>
        /// 退货数量
        /// </summary>
        [Required]
        public double Quantity { get; set; }

        public int Sort { get; set; }

        protected PurchaseReturnDetail()
        {
        }

        public PurchaseReturnDetail(Guid id) : base(id)
        {
        }
     
    }
}
