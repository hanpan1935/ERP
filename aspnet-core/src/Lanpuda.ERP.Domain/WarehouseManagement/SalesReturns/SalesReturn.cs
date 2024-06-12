using Lanpuda.ERP.SalesManagement.SalesReturnApplies;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns
{
    /// <summary>
    /// 销售退货
    /// </summary>
    public class SalesReturn : AuditedAggregateRoot<Guid>
    {
        public Guid SalesReturnApplyDetailId { get; set; }
        public SalesReturnApplyDetail SalesReturnApplyDetail { get; set; }


        public string Number { get; set; }

        /// <summary>
        /// 入库人
        /// </summary>
        public Guid? KeeperUserId { get; set; }
        public IdentityUser KeeperUser { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


        /// <summary>
        /// 入库状态  false待入库  true已入库
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime? SuccessfulTime { get; set; }

        /// <summary>
        /// 入库明细
        /// </summary>
        public List<SalesReturnDetail> Details { get; set; }

        public IdentityUser Creator { get; set; }
        protected SalesReturn()
        {
        }

        public SalesReturn(Guid id) : base(id)
        {
            Details = new List<SalesReturnDetail>();
        }
    }
}
