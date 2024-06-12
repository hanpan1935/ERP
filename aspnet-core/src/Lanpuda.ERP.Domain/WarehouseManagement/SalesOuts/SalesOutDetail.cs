using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.SalesManagement.ShipmentApplies;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts
{
    public class SalesOutDetail : AuditedAggregateRoot<Guid>
    {
        public Guid SalesOutId { get; set; }
        public SalesOut SalesOut { get; set; }


        /// <summary>
        /// ศ฿ำเสýพÝ
        /// </summary>
        //public Guid ProductId { get; set; }
        //public Product Product { get; set; }



        public Guid LocationId { get; set; }
        public Location Location { get; set; }


        [MaxLength(128)]
        public string Batch { get; set; }


        [Required]
        public double Quantity { get; set; }

        public int Sort { get; set; }

        protected SalesOutDetail()
        {
        }

        public SalesOutDetail(Guid id) : base(id)
        {

        }
    }
}
