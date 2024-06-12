using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.QualityManagement.ProcessInspections
{
    public class ProcessInspection : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }

        public Guid WorkOrderStorageApplyId { get; set; }
        public WorkOrderStorageApply WorkOrderStorageApply { get; set; }


        /// <summary>
        /// 不良数量
        /// </summary>
        public double BadQuantity { get; set; }


        /// <summary>
        /// 情况描述
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// true 确认  false 暂存
        /// </summary>
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedTime { get; set; }
        public Guid? ConfirmeUserId { get; set; }
        public IdentityUser ConfirmeUser { get; set; }


        public IdentityUser Creator { get; set; }

        protected ProcessInspection()
        {
        }

        public ProcessInspection(Guid id) : base(id)
        {
        }
    }
}
