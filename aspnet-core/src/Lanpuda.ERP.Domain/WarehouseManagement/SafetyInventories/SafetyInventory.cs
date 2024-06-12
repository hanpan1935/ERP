using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories
{
    public class SafetyInventory : AuditedAggregateRoot<Guid>
    {
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public double? MinQuantity { get; set; }

        public double? MaxQuantity { get; set; }

        public IdentityUser Creator { get; set; }
        protected SafetyInventory()
        {
        }

        public SafetyInventory( Guid id)
        {
        }
    }
}
