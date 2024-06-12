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

namespace Lanpuda.ERP.SalesManagement.SalesOrders
{
    /// <summary>
    /// 销售订单
    /// </summary>
    public class SalesOrder : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 订单编号*
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Number { get; set; }


        /// <summary>
        /// 客户Id*
        /// </summary>
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }


        /// <summary>
        /// 要求交期
        /// </summary>
        public DateTime? RequiredDate { get; set; }

        /// <summary>
        /// 承诺交期
        /// </summary>
        public DateTime? PromisedDate { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public SalesOrderType OrderType { get; set; }

        /// <summary>
        /// 订单说明
        /// </summary>
        [MaxLength(256, ErrorMessage = "最长256个字符")]
        public string Description { get; set; }


        /// <summary>
        /// 关闭状态
        /// </summary>
        public SalesOrderCloseStatus CloseStatus { get; set; }


        public IdentityUser Creator { get; set; }


        /// <summary>
        /// 是否确认(暂存,提交)
        /// </summary>
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmeTime { get; set; }
        public Guid? ConfirmeUserId { get; set; }
        public IdentityUser ConfirmeUser { get; set; }



        /// <summary>
        /// 订单明细
        /// </summary>
        public List<SalesOrderDetail> Details { get; set; }


        protected SalesOrder()
        {
        }

        public SalesOrder(Guid id) : base(id)
        {
            Details = new List<SalesOrderDetail>();
        }
    }
}
