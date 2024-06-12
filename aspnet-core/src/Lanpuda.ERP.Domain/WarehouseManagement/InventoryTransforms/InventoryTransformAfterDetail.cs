using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.WarehouseManagement.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms
{
    public class InventoryTransformAfterDetail : AuditedAggregateRoot<Guid>
    {
        public Guid InventoryTransformId { get; set; }
        public InventoryTransform InventoryTransform { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public string Batch { get; set; }

        public double Quantity { get; set; }

        public double Price { get; set; }

        protected InventoryTransformAfterDetail()
        {
        }

        public InventoryTransformAfterDetail(Guid id) : base(id)
        {
            
        }
    }
}
