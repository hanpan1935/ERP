using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.QualityManagement.ProcessInspections;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies
{
    public class WorkOrderStorageApply : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }


        public Guid WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }

        public double Quantity { get; set; }

        public IdentityUser Creator { get; set; }


        public string Remark { get; set; }

        /// <summary>
        /// 是否确认(暂存,提交)
        /// </summary>
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedTime { get; set; }
        public Guid? ConfirmedUserId { get; set; }
        public IdentityUser ConfirmedUser { get; set; }



        /// <summary>
        /// 1对1 生产入库
        /// </summary>
        public WorkOrderStorage WorkOrderStorage { get; set; }

        /// <summary>
        /// 1对1 过程检验
        /// </summary>
        public ProcessInspection ProcessInspection { get; set; }


        protected WorkOrderStorageApply()
        {
        }

        public WorkOrderStorageApply(Guid id) : base(id)
        {
        }
    }
}
