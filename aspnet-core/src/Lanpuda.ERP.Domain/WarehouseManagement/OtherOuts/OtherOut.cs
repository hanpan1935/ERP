using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts
{
    public class OtherOut : AuditedAggregateRoot<Guid>
    {
        [Required]
        [MaxLength(128)]
        [Display(Name = "入库单号")]
        public string Number { get; set; }


        [Display(Name = "入库人")]
        public Guid? KeeperUserId { get; set; }
        [ForeignKey(nameof(KeeperUserId))]
        public IdentityUser KeeperUser { get; set; }


        [Required]
        [Display(Name = "出库原因")]
        [MaxLength(256)]
        public string Description { get; set; }


        /// <summary>
        /// 入库状态  false待入库  true已入库
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime? SuccessfulTime { get; set; }

        public List<OtherOutDetail> Details { get; set; }

        public IdentityUser Creator { get; set; }
        protected OtherOut()
        {
        }

        public OtherOut(Guid id) : base(id)
        {
        }
    }
}
