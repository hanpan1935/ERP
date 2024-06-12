using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Lanpuda.ERP.SalesManagement.ShipmentApplies;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts
{
    /// <summary>
    /// 销售出库
    /// </summary>
    public class SalesOut : AuditedAggregateRoot<Guid>
    {

        public Guid ShipmentApplyDetailId { get; set; }
        public ShipmentApplyDetail ShipmentApplyDetail { get; set; }



        public string Number { get; set; }

        /// <summary>
        /// 发货人
        /// </summary>
        public Guid? KeeperUserId { get; set; }
        public IdentityUser KeeperUser { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 出库状态  
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime? SuccessfulTime { get; set; }

        /// <summary>
        /// 出库明细
        /// </summary>
        public List<SalesOutDetail> Details { get; set; }



        public IdentityUser Creator { get; set; }

        protected SalesOut()
        {
        }

        public SalesOut(Guid id) : base(id)
        {
            Details = new List<SalesOutDetail>();
        }
    }
}
