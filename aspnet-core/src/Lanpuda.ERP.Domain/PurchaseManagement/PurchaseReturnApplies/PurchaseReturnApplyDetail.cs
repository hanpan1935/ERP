using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies
{
    public class PurchaseReturnApplyDetail : AuditedAggregateRoot<Guid>
    {
        public Guid PurchaseReturnApplyId { get; set; }
        public PurchaseReturnApply PurchaseReturnApply { get; set; }


        public Guid PurchaseStorageDetailId { get; set; }
        public PurchaseStorageDetail PurchaseStorageDetail { get; set; }


        public PurchaseReturn PurchaseReturn { get; set; }
        /// <summary>
        /// 退货数量
        /// </summary>
        [Required]
        public double Quantity { get; set; }
        

        public string Remark { get; set; }

        public int Sort { get; set; }

        protected PurchaseReturnApplyDetail()
        {
        }

        public PurchaseReturnApplyDetail(Guid id) : base(id)
        {
        }
    }
}
