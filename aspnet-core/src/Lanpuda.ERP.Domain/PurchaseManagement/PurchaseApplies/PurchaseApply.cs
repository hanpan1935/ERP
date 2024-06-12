using Lanpuda.ERP.ProductionManagement.Mpses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies
{
    public class PurchaseApply : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }

        public PurchaseApplyType ApplyType { get; set; }

        public Guid? MpsId { get; set; }
        public Mps Mps { get; set; }

        public string Remark { get; set; }


        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedTime { get; set; }
        public Guid? ConfirmeUserId { get; set; }
        public IdentityUser ConfirmeUser { get; set; }


        public bool IsAccept { get; set; }
        public DateTime? AcceptTime { get; set; }
        public Guid? AcceptUserId { get; set; }
        public IdentityUser AcceptUser { get; set; }

        public List<PurchaseApplyDetail> Details { get; set; }

        public IdentityUser Creator { get; set; }

        protected PurchaseApply()
        {
        }

        public PurchaseApply(Guid id) : base(id)
        {
            Details = new List<PurchaseApplyDetail>();
        }
    }
}
