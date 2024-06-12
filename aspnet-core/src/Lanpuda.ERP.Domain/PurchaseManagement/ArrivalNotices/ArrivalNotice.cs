using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.QualityManagement.ArrivalInspections;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices
{
    /// <summary>
    /// 来料通知
    /// </summary>
    public class ArrivalNotice : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }


        /// <summary>
        /// 来料时间
        /// </summary>
        public DateTime ArrivalTime { get; set; }
      


        public string Remark { get; set; }

        public IdentityUser Creator { get; set; }


        /// <summary>
        /// 是否确认(暂存,提交)
        /// </summary>
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedTime { get; set; }
        public Guid? ConfirmeUserId { get; set; }
        public IdentityUser ConfirmeUser { get; set; }


        public List<ArrivalNoticeDetail> Details { get; set; }

       


        protected ArrivalNotice()
        {
        }

        public ArrivalNotice(Guid id) : base(id)
        {
            Details = new List<ArrivalNoticeDetail>();
        }
    }
}
