using Lanpuda.ERP.PurchaseManagement.Suppliers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Timing;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders
{
    public class PurchaseOrder : AuditedAggregateRoot<Guid>
    {
        [Required]
        public Guid SupplierId { get; set; }
        [ForeignKey(nameof(SupplierId))]
        public Supplier Supplier { get; set; }

        /// <summary>
        /// 订单编号- PO开头
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Number { get; set; }


        /// <summary>
        /// 要求交期
        /// </summary>
        [Column(TypeName = "date")]
        [Required]
        public DateTime RequiredDate { get; set; }

        /// <summary>
        /// 承诺交期
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? PromisedDate { get; set; }


        /// <summary>
        /// 订单来源
        /// </summary>
        public PurchaseOrderType OrderType { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        [MaxLength(128)]
        public string Contact { get; set; }


        /// <summary>
        /// 收货人电话
        /// </summary>
        [MaxLength(128)]
        public string ContactTel { get; set; }

        /// <summary>
        /// 送货地址
        /// </summary>
        [MaxLength(256)]
        public string ShippingAddress { get; set; }


        //订单备注
        [MaxLength(256)]
        public string Remark { get; set; }

        /// <summary>
        /// 是否确认(暂存,提交)
        /// </summary>
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedTime { get; set; }
        public Guid? ConfirmeUserId { get; set; }
        public IdentityUser ConfirmeUser { get; set; }

        /// <summary>
        /// 关闭状态
        /// </summary>
        public PurchaseOrderCloseStatus CloseStatus { get; set; }


        public List<PurchaseOrderDetail> Details { get; set; }

        public IdentityUser Creator { get; set; }
        protected PurchaseOrder()
        {
        }

        public PurchaseOrder(Guid id) : base(id)
        {
            Details = new List<PurchaseOrderDetail>();
        }
    }
}
