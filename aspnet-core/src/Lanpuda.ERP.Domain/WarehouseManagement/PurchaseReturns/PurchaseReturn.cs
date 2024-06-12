using Lanpuda.ERP.PurchaseManagement.Suppliers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns
{
    public class PurchaseReturn : AuditedAggregateRoot<Guid>
    {
        public Guid PurchaseReturnApplyDetailId { get; set; }
        public PurchaseReturnApplyDetail PurchaseReturnApplyDetail { get; set; }


        [Required]
        [MaxLength(128)]
        public string Number { get; set; }

        //问题描述
        [MaxLength(256)]
        public string Remark { get; set; }

        /// <summary>
        /// 出库人
        /// </summary>
        public Guid? KeeperUserId { get; set; }
        public IdentityUser KeeperUser { get; set; }


        /// <summary>
        /// 入库状态  false待入库  true已入库
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime? SuccessfulTime { get; set; }


        public List<PurchaseReturnDetail> Details { get; set; }
        public IdentityUser Creator { get; set; }
        protected PurchaseReturn()
        {
        }

        public PurchaseReturn(Guid id) : base(id)
        {
            Details = new List<PurchaseReturnDetail>();
        }
    }
}
