using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies
{
    public class MaterialApply : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }

        public string Remark { get; set; }

        public Guid WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }

        public IdentityUser Creator { get; set; }


        /// <summary>
        /// 确认人
        /// </summary>
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedTime { get; set; }
        public Guid? ConfirmedUserId { get; set; }
        public IdentityUser ConfirmedUser { get; set; }

        public List<MaterialApplyDetail> Details { get; set; }

      

        protected MaterialApply()
        {
        }

        public MaterialApply(Guid id) : base(id)
        {
            Details = new List<MaterialApplyDetail>();
        }
    }
}
