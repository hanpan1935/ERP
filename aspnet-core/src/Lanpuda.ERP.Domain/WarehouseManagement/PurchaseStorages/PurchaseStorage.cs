using Lanpuda.ERP.PurchaseManagement.ArrivalNotices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages
{
    public class PurchaseStorage : AuditedAggregateRoot<Guid>
    {
        public Guid ArrivalNoticeDetailId { get; set; }
        public ArrivalNoticeDetail ArrivalNoticeDetail { get; set; }


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
        public DateTime? SuccessfulTime { get; set; }

    
        public List<PurchaseStorageDetail> Details { get; set; }


        public IdentityUser Creator { get; set; }

        protected PurchaseStorage()
        {
        }

        public PurchaseStorage(Guid id) : base(id)
        {
            Details = new List<PurchaseStorageDetail>();
        }
    }
}
