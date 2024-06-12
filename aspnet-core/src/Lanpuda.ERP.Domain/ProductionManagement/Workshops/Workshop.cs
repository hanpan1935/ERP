using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.ProductionManagement.Workshops
{
    /// <summary>
    /// 生产车间
    /// </summary>
    public class Workshop : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public IdentityUser Creator { get; set; }

        protected Workshop()
        {

        }

        public Workshop(Guid id) : base(id)
        {

        }
    }
}
