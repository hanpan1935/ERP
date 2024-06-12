using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.SalesManagement.Customers;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies
{
    public class PurchaseReturnApply : AuditedAggregateRoot<Guid>
    {
        [Required]
        [MaxLength(128)]
        public string Number { get; set; }


        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }


        public IdentityUser Creator { get; set; }

        /// <summary>
        /// 退货原因
        /// </summary>
        public PurchaseReturnReason ReturnReason { get; set; }


        /// <summary>
        /// 问题描述
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string Description { get; set; }



        [MaxLength(256)]
        public string Remark { get; set; }


        /// <summary>
        /// true 确认  false 暂存
        /// </summary>
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedTime { get; set; }
        public Guid? ConfirmeUserId { get; set; }
        public IdentityUser ConfirmeUser { get; set; }


        public List<PurchaseReturnApplyDetail> Details { get; set; }


    


        protected PurchaseReturnApply()
        {
        }

        public PurchaseReturnApply(Guid id) : base(id)
        {
            Details = new List<PurchaseReturnApplyDetail>();
        }
    }
}
