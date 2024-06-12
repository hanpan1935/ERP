using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.ProductionManagement.Boms
{
    public class Bom : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }

        public List<BomDetail> Details { get; set; }

        public IdentityUser Creator { get; set; }

        protected Bom()
        {
        }

        public Bom(Guid id) : base(id)
        {
            Details = new List<BomDetail>();
        }
    }
}
