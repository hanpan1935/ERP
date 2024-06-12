using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.SalesManagement.Customers;
using Volo.Abp.Identity;
using Lanpuda.ERP.WarehouseManagement.SalesReturns;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies
{
    /// <summary>
    /// 销售退货
    /// </summary>
    public class SalesReturnApply : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }


        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        /// <summary>
        /// 退货原因
        /// </summary>
        public SalesReturnReason Reason { get; set; }



        /// <summary>
        /// 商品是否要退回
        /// 如果否 不创建退货入库单, 但是创建应收明细
        /// </summary>
        public bool IsProductReturn { get; set; }

        /// <summary>
        /// 具体描述
        /// </summary>
        [MaxLength(256)]
        public string Description { get; set; }


        /// <summary>
        /// 是否确认(暂存,提交)
        /// </summary>
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmeTime { get; set; }
        public Guid? ConfirmeUserId { get; set; }
        public IdentityUser ConfirmeUser { get; set; }

        public List<SalesReturnApplyDetail> Details { get; set; }




        public IdentityUser Creator { get; set; }

        protected SalesReturnApply()
        {
        }

        public SalesReturnApply(Guid id) : base(id)
        {
            Details = new List<SalesReturnApplyDetail>();
        }
    }
}
