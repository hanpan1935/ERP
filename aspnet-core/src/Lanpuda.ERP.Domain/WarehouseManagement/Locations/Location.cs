using Lanpuda.ERP.WarehouseManagement.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.WarehouseManagement.Locations
{
    public class Location : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 仓库ID
        /// </summary>
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }


        /// <summary>
        /// 库位编号
        /// </summary>
        public string Number { get; set; }


        /// <summary>
        /// 库位名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


        public IdentityUser Creator { get; set; }

        protected Location()
        {
        }

        public Location(Guid id) : base(id)
        {

        }
    }
}
