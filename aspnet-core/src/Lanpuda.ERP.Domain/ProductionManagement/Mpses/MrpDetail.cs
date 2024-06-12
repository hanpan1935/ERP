using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.ProductionManagement.Mrps;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.ProductionManagement.Mpses
{
    public class MrpDetail : AuditedAggregateRoot<Guid>
    {
        public Guid MpsId { get; set; }
        public Mps Mps { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime RequiredDate { get; set; }

        public double Quantity { get; set; }


        protected MrpDetail()
        {
        }

        public MrpDetail(Guid id) : base(id)
        {

        }
    }
}
