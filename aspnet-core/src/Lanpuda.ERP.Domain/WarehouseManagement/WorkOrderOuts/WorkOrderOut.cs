using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.ProductionManagement.MaterialApplies;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts
{
    /// <summary>
    /// 生产领料
    /// </summary>
    public class WorkOrderOut : AuditedAggregateRoot<Guid>
    {
        public Guid MaterialApplyDetailId { get; set; }
        public MaterialApplyDetail MaterialApplyDetail { get; set; }


        [Required]
        [MaxLength(128)]
        [Display(Name = "出库单号")]
        public string Number { get; set; }


        [Display(Name = "出库人")]
        public Guid? KeeperUserId { get; set; }
        [ForeignKey(nameof(KeeperUserId))]
        public IdentityUser KeeperUser { get; set; }



        [Display(Name = "备注")]
        [MaxLength(256)]
        public string Remark { get; set; }


        /// <summary>
        /// 出库状态  false待出库  true已出库
        /// </summary>
        public bool IsSuccessful  { get; set; }
        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime? SuccessfulTime  { get; set; }


        public List<WorkOrderOutDetail> Details { get; set; }

        public IdentityUser Creator { get; set; }

        protected WorkOrderOut()
        {
        }

        public WorkOrderOut(Guid id) : base(id)
        {
            Details = new List<WorkOrderOutDetail>();
        }
    }
}
