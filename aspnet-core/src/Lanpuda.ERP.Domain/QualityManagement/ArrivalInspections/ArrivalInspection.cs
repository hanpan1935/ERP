using Lanpuda.ERP.PurchaseManagement.ArrivalNotices;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.QualityManagement.ArrivalInspections
{
    public class ArrivalInspection : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }


        public Guid ArrivalNoticeDetailId { get; set; }
        public ArrivalNoticeDetail ArrivalNoticeDetail { get; set; }

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
        protected ArrivalInspection()
        {
        }

        public ArrivalInspection(Guid id
        ) : base(id)
        {
        }
    }
}
