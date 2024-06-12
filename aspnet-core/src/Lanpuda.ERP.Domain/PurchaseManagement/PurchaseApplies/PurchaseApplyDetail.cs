using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.BasicData.Products;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies
{
    public class PurchaseApplyDetail : AuditedAggregateRoot<Guid>
    {
        public Guid PurchaseApplyId { get; set; }
        public PurchaseApply PurchaseApply { get; set; }


        [Required]
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        /// <summary>
        /// 申请数量
        /// </summary>
        [Required]
        public double Quantity { get; set; }

        /// <summary>
        /// 到货日期
        /// </summary>
        public DateTime? ArrivalDate { get; set; }


        public int Sort { get; set; }

        protected PurchaseApplyDetail()
        {
        }

        public PurchaseApplyDetail(Guid id) : base(id)
        {
        }
    }
}
