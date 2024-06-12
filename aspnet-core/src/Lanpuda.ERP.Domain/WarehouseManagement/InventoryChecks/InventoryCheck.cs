using Lanpuda.ERP.WarehouseManagement.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks
{
    /// <summary>
    /// ø‚¥Ê≈Ãµ„
    /// </summary>
    public class InventoryCheck : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }

        public DateTime CheckDate { get; set; }

        public string Discription { get; set; }

        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }


        public Guid? KeeperUserId { get; set; }
        public IdentityUser KeeperUser { get; set; }
        public bool IsSuccessful { get; set; }
        public DateTime? SuccessfulTime { get; set; }
        


        public List<InventoryCheckDetail> Details { get; set; }


        public IdentityUser Creator { get; set; }

        protected InventoryCheck()
        {
        }

        public InventoryCheck(Guid id) : base(id)
        {
        }
    }
}
