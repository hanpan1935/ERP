using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.BasicData.Products;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices
{
    public class PurchasePriceDetail : AuditedAggregateRoot<Guid>
    {
        [Required]
        public Guid PurchasePriceId { get; set; }
        [ForeignKey(nameof(PurchasePriceId))]
        public PurchasePrice PurchasePrice { get; set; }


        [Required]
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }


        /// <summary>
        ///  采购价格含税
        /// </summary>
        [Required]
        public double Price { get; set; }


        /// <summary>
        /// 税率
        /// </summary>
        [Required]
        public double TaxRate { get; set; }


        public int Sort { get; set; }
        protected PurchasePriceDetail()
        {
        }

        public PurchasePriceDetail(Guid id) : base(id)
        {
         
        }
    }
}
