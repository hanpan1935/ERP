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

namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves
{
    public class InventoryMoveDetail : AuditedAggregateRoot<Guid>
    {
        [Required]
        public Guid InventoryMoveId { get; set; }
        public InventoryMove InventoryMove { get; set; }


        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public Guid OutLocationId { get; set; }
        public Location OutLocation { get; set; }


        [MaxLength(128)]
        public string Batch { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        [Required]
        public double Quantity { get; set; }


        public Guid InLocationId { get; set; }
        public Location InLocation { get; set; }

        public int Sort { get; set; }

        protected InventoryMoveDetail()
        {
        }

        public InventoryMoveDetail(Guid id) : base(id)
        {
           
        }
    }
}
