using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.ProductionManagement.Mpses;
using Lanpuda.ERP.ProductionManagement.Workshops;
using Lanpuda.ERP.QualityManagement.ProcessInspections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders
{
    public class WorkOrder : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }

        //public WorkOrderType WorkOrderType { get; set; }

        public Guid? WorkshopId { get; set; }
        public Workshop Workshop { get; set; }


        public Guid MpsId { get; set; }
        public Mps Mps { get; set; }


        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public double Quantity { get; set; }

        /// <summary>
        /// 开工日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 交货日期
        /// </summary>
        public DateTime? CompletionDate { get; set; }



        /// <summary>
        /// 
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }


        /// <summary>
        /// 是否确认(暂存,提交)
        /// </summary>
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedTime { get; set; }
        public Guid? ConfirmedUserId { get; set; }
        public IdentityUser ConfirmedUser { get; set; }


        public List<WorkOrderMaterial> StandardMaterialDetails { get; set; }
  

        


        public IdentityUser Creator { get; set; }

        protected WorkOrder()
        {
        }

        public WorkOrder(Guid id) : base(id)
        {
            //StandardMaterialDetails = new List<WorkOrderMaterial>();
            //MaterialApplyList = new List<MaterialApply>();
            //MaterialReturnApplyList = new List<MaterialReturnApply>();
            StandardMaterialDetails = new List<WorkOrderMaterial>();
        }
    }
}
