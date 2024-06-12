using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages
{
    public class PurchaseStorageDetail : AuditedAggregateRoot<Guid>
    {
        [Required]
        public Guid PurchaseStorageId { get; set; }
        [ForeignKey(nameof(PurchaseStorageId))]
        public PurchaseStorage PurchaseStorage { get; set; }


        /// <summary>
        /// 冗余数据
        /// </summary>
        //public Guid ProductId { get; set; }
        //public Product Product { get; set; }



        public Guid LocationId { get; set; }
        public Location Location { get; set; }


        [MaxLength(128)]
        public string Batch { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        [Required]
        public double Quantity { get; set; }


        public int Sort { get; set; }

        protected PurchaseStorageDetail()
        {
        }

        public PurchaseStorageDetail(Guid id) : base(id)
        {
            
        }
    }
}
