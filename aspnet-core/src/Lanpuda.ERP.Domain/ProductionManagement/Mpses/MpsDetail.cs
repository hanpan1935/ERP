using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.ProductionManagement.Mpses
{
    public class MpsDetail : AuditedAggregateRoot<Guid>
    {
        public Guid MpsId { get; set; }
        public Mps Mps { get; set; }

        public DateTime ProductionDate { get; set; }

        public double Quantity { get; set; }

        public string Remark { get; set; }

        protected MpsDetail()
        {
        }

        public MpsDetail(Guid id) : base(id)
        {
        }
      
    }
}
