using Lanpuda.ERP.WarehouseManagement.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses
{
    /// <summary>
    /// 仓库
    /// </summary>
    public class Warehouse : AuditedAggregateRoot<Guid>
    {

        public string Number { get; set; }


        public string Name { get; set; }


        public string Remark { get; set; }


        public List<Location> Locations { get; set; }

        public IdentityUser Creator { get; set; }
        protected Warehouse()
        {
        }

        public Warehouse(Guid id) : base(id)
        {
            Locations = new List<Location>();
        }
    }
}
