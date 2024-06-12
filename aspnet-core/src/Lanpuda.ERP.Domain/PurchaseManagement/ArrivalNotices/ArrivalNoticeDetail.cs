using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Lanpuda.ERP.QualityManagement.ArrivalInspections;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices
{
    public class ArrivalNoticeDetail : AuditedAggregateRoot<Guid>
    {
        public Guid ArrivalNoticeId { get; set; }
        public ArrivalNotice ArrivalNotice { get; set; }


        public Guid PurchaseOrderDetailId { get; set; }
        public PurchaseOrderDetail PurchaseOrderDetail { get; set; }


        public PurchaseStorage PurchaseStorage { get; set; }

        /// <summary>
        /// 来料数量
        /// </summary>
        public double Quantity { get; set; }


        public ArrivalInspection ArrivalInspection { get; set; }


        public int Sort { get; set; }

        protected ArrivalNoticeDetail()
        {
        }

        public ArrivalNoticeDetail(Guid id) : base(id)
        {
        }
    }
}
