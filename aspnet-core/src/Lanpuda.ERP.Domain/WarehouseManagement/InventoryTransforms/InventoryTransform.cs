using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.WarehouseManagement.InventoryTransforms
{
    /// <summary>
    /// 形态转换
    /// </summary>
    public class InventoryTransform : AuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }

        public string Reason { get; set; }

        /// <summary>
        /// 执行人
        /// </summary>
        public Guid? KeeperUserId { get; set; }
        public IdentityUser KeeperUser { get; set; }
       
        public bool IsSuccessful { get; set; }

        public DateTime? SuccessfulTime { get; set; }


        public List<InventoryTransformBeforeDetail> BeforeDetails { get; set; }
        public List<InventoryTransformAfterDetail> AfterDetails { get; set; }

        public IdentityUser Creator { get; set; }
        protected InventoryTransform()
        {
        }

        public InventoryTransform(Guid id) : base(id)
        {
        }
    }
}
