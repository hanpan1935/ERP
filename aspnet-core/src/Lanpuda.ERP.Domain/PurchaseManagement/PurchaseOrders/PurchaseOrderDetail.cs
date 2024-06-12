using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders
{
    public class PurchaseOrderDetail : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 所属采购订单
        /// </summary>
        [Required]
        public Guid PurchaseOrderId { get; set; }
        [ForeignKey(nameof(PurchaseOrderId))]
        public PurchaseOrder PurchaseOrder { get; set; }


        [Required]
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }



        [Column(TypeName = "date")]
        [Required]
        public DateTime PromiseDate { get; set; }

        /// <summary>
        /// 采购数量
        /// </summary>
        [Required]
        public double Quantity { get; set; }

        /// <summary>
        /// 采购单价含税*
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        ///  税率*
        /// </summary>
        [Required]
        public double TaxRate { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        public string Remark { get; set; }


        /// <summary>
        /// 列表顺序
        /// </summary>
        public int Sort { get; set; }


        protected PurchaseOrderDetail()
        {
            
        }

        public PurchaseOrderDetail(Guid id) : base(id)
        {
          
        }
    }
}
