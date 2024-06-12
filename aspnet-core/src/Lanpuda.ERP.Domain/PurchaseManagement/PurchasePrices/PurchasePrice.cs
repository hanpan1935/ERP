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

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices
{
    public class PurchasePrice : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 供应商Id
        /// </summary>
        [Required]
        public Guid SupplierId { get; set; }
        [ForeignKey(nameof(SupplierId))]
        public Supplier Supplier { get; set; }



        /// <summary>
        /// 报价单号
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Number { get; set; }

        /// <summary>
        /// 平均交期（天）
        /// </summary>
        [Required]
        public int AvgDeliveryDate { get; set; }


        /// <summary>
        /// 报价日期
        /// </summary>
        [Column(TypeName = "Date")]
        [Required]
        public DateTime QuotationDate { get; set; }


        [MaxLength(256)]
        public string Remark { get; set; }


        public List<PurchasePriceDetail> Details { get; set; }


        public IdentityUser Creator { get; set; }
        protected PurchasePrice()
        {
        }

        public PurchasePrice(Guid id) : base(id)
        {
            Details = new List<PurchasePriceDetail>();
        }
    }
}
