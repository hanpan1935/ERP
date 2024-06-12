using Lanpuda.ERP.ProductionManagement.Mpses;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.QualityManagement.FinalInspections
{
    public class FinalInspection : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }

        public Guid MpsId { get; set; }
        public Mps Mps { get; set; }


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

        protected FinalInspection()
        {
        }

        public FinalInspection(Guid id) : base(id)
        {
        }
    }
}
