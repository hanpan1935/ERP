using Lanpuda.ERP.SalesManagement.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.SalesManagement.SalesPrices
{
    public class SalesPrice : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 报价单号
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Number { get; set; }

        /// <summary>
        /// 客户Id
        /// </summary>
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        /// 
        [Column(TypeName = "date")]
        public DateTime ValidDate { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }


        public List<SalesPriceDetail> Details { get; set; }


        public IdentityUser Creator { get; set; }
        protected SalesPrice()
        {
        }

        public SalesPrice(Guid id) : base(id)
        {
        }
    }
}
