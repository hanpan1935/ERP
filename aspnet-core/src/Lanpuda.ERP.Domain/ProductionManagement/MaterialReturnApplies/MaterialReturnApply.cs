using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies
{
    public class MaterialReturnApply : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }

        public string Remark { get; set; }

        public IdentityUser Creator { get; set; }


        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedTime { get; set; }
        public Guid? ConfirmedUserId { get; set; }
        public IdentityUser ConfirmedUser { get; set; }


        public List<MaterialReturnApplyDetail> Details { get; set; }

     


        protected MaterialReturnApply()
        {
        }

        public MaterialReturnApply(Guid id) : base(id)
        {
            Details = new List<MaterialReturnApplyDetail>();
        }
    }
}
