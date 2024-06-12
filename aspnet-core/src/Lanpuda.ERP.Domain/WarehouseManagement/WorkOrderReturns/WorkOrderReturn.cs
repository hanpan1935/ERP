using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Volo.Abp.Identity;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns
{
    /// <summary>
    /// 生产退料
    /// </summary>
    public class WorkOrderReturn : AuditedAggregateRoot<Guid>
    {
        public Guid MaterialReturnApplyDetailId { get; set; }
        public MaterialReturnApplyDetail MaterialReturnApplyDetail { get; set; }


        [Required]
        [MaxLength(128)]
        [Display(Name = "入库单号")]
        public string Number { get; set; }


        [Display(Name = "入库人")]
        public Guid? KeeperUserId { get; set; }
        [ForeignKey(nameof(KeeperUserId))]
        public IdentityUser KeeperUser { get; set; }



        [Display(Name = "备注")]
        [MaxLength(256)]
        public string Remark { get; set; }


        /// <summary>
        /// 入库状态  false待入库  true已入库
        /// </summary>
        public bool IsSuccessful  { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime? SuccessfulTime  { get; set; }


        public List<WorkOrderReturnDetail> Details { get; set; }


        public IdentityUser Creator { get; set; }


        protected WorkOrderReturn()
        {
        }

        public WorkOrderReturn(Guid id) : base(id)
        {
            Details = new List<WorkOrderReturnDetail>();
        }
    }
}
