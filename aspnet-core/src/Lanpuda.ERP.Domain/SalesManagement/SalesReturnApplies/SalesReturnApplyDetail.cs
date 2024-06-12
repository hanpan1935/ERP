using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.ERP.WarehouseManagement.SalesOuts;
using Lanpuda.ERP.WarehouseManagement.SalesReturns;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies
{
    public class SalesReturnApplyDetail : AuditedAggregateRoot<Guid>
    {
        public Guid SalesReturnApplyId { get; set; }
        public SalesReturnApply SalesReturnApply { get; set; }



        public Guid SalesOutDetailId { get; set; }
        public SalesOutDetail SalesOutDetail { get; set; }


        /// <summary>
        /// 一对一 销售退货入库
        /// </summary>
        public SalesReturn SalesReturn { get; set; }


        /// <summary>
        /// 退货数量
        /// </summary>
        public double Quantity { get; set; }


        /// <summary>
        /// 退货说明
        /// </summary>
        public string Description { get; set; }



        public int Sort { get; set; }

        protected SalesReturnApplyDetail()
        {
        }

        public SalesReturnApplyDetail(Guid id) : base(id)
        {
        }
    }
}
