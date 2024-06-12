using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.WarehouseManagement.Inventories;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts
{
    public class OtherOutDetail : AuditedAggregateRoot<Guid>
    {
        [Required]
        public Guid OtherOutId { get; set; }
        public OtherOut OtherOut { get; set; }


        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public Guid LocationId { get; set; }
        public Location Location { get; set; }


        [MaxLength(128)]
        public string Batch { get; set; }

        /// <summary>
        /// 出库数量
        /// </summary>
        [Required]
        public double Quantity { get; set; }


        public int Sort { get; set; }
        protected OtherOutDetail()
        {
        }

        public OtherOutDetail(Guid id) : base(id)
        {

        }
    }
}
