using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.ProductionManagement.Mrps;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies;
using Lanpuda.ERP.QualityManagement.FinalInspections;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.ProductionManagement.Mpses
{
    public class Mps : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }

        /// <summary>
        /// 客户订单 内部订单  安全库存
        /// </summary>
        public MpsType MpsType { get; set; }


        public Guid? SalesOrderDetailId { get; set; }
        public SalesOrderDetail SalesOrderDetail { get; set; }

        /// <summary>
        /// 开工时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompleteDate { get; set; }


        /// <summary>
        /// 生产产品
        /// </summary>
        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        /// <summary>
        /// 计划数量
        /// </summary>
        public double Quantity { get; set; }

       


        public string Remark { get; set; }

        public PurchaseApply PurchaseApply { get; set; }


        /// <summary>
        /// 是否确认(暂存,提交)
        /// </summary>
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedTime { get; set; }
        public Guid? ConfirmedUserId { get; set; }
        public IdentityUser ConfirmedUser { get; set; }
       
        
        public List<MpsDetail> Details { get; set; }
        public List<MrpDetail> MrpDetails { get; set; }
        public List<WorkOrder> WorkOrderDetails { get; set; }


        public FinalInspection FinalInspection { get; set; }
       


        public IdentityUser Creator { get; set; }
        protected Mps()
        {
        }

        public Mps(Guid id) : base(id)
        {
            Details = new List<MpsDetail>();
            MrpDetails = new List<MrpDetail>();
            WorkOrderDetails = new List<WorkOrder>();
        }
    }
}
