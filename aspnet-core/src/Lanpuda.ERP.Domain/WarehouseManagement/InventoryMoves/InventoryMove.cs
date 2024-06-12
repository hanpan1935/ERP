using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.BasicData.Products;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves
{
    public class InventoryMove : AuditedAggregateRoot<Guid>
    {
        [Required]
        [MaxLength(128)]
        [Display(Name = "调拨单号")]
        public string Number { get; set; }



        [Display(Name = "调拨人")]
        public Guid? KeeperUserId { get; set; }
        [ForeignKey(nameof(KeeperUserId))]
        public IdentityUser KeeperUser { get; set; }


        /// <summary>
        /// 调拨原因
        /// </summary>
        public string Reason { get; set; }



        [Display(Name = "备注")]
        [MaxLength(256)]
        public string Remark { get; set; }


        /// <summary>
        /// 调拨状态  
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 调拨时间
        /// </summary>
        public DateTime? SuccessfulTime { get; set; }


        public List<InventoryMoveDetail> Details { get; set; }

        public IdentityUser Creator { get; set; }
        protected InventoryMove()
        {
        }

        public InventoryMove(Guid id) : base(id)
        {
            Details = new List<InventoryMoveDetail>();
        }
    }
}
