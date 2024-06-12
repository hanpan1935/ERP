using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.WarehouseManagement.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks
{
    public class InventoryCheckDetail : AuditedAggregateRoot<Guid>
    {
        public Guid InventoryCheckId { get; set; }
        public InventoryCheck InventoryCheck { get; set; }


        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public Guid LocationId { get; set; }
        public Location Location { get; set; }


        public string Batch { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public double InventoryQuantity { get; set; }

        /// <summary>
        /// 盘盈盘亏
        /// </summary>
        public InventoryCheckDetailType CheckType { get; set; }

        /// <summary>
        /// 盈亏数量
        /// </summary>
        public double CheckQuantity { get; set; }


        public int Sort { get; set; }
        protected InventoryCheckDetail()
        {
        }

        public InventoryCheckDetail(Guid id) : base(id)
        {
        }
    }
}
