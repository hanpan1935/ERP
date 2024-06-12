using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.ProductionManagement.Boms
{
    public class BomDetail : AuditedAggregateRoot<Guid>
    {
        public Guid BomId { get; set; }
        public Bom Bom { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        /// <summary>
        /// 标准用量
        /// </summary>
        public double Quantity { get; set; }

        public string Remark { set; get; }

        public int Sort { get; set; }

        protected BomDetail()
        {
        }

        public BomDetail(Guid id) : base(id)
        {

        }
    }
}
