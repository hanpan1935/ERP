using Lanpuda.ERP.SalesManagement.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.ERP.WarehouseManagement.SalesOuts;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies
{
    public class ShipmentApply : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }


        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ConsigneeTel { get; set; }




        /// <summary>
        /// 是否确认(暂存,提交)
        /// </summary>
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmeTime { get; set; }
        public Guid? ConfirmeUserId { get; set; }
        public IdentityUser ConfirmeUser { get; set; }

        public List<ShipmentApplyDetail> Details { get; set; }



        public IdentityUser Creator { get; set; }
        protected ShipmentApply()
        {
        }

        public ShipmentApply(Guid id) : base(id)
        {
            Details = new List<ShipmentApplyDetail>();
        }
    }
}
