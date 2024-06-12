using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.QualityManagement.ConcessionReviews
{
    /// <summary>
    /// 让步评审
    /// </summary>
    public class ConcessionReview : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 让步单号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 让步来源
        /// </summary>
        public ConcessionType ConcessionType { get; set; }

        /// <summary>
        /// 来料检验 过程检验 产品终检 不良评审的单号, 根据ConcessionType判断是具体的哪一个
        /// </summary>
        public string SourceNumber { get; set; }


        /// <summary>
        /// 细节描述
        /// </summary>
        public string Description { get; set; }

        public IdentityUser Creator { get; set; }
    }
}
