using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.SalesManagement.Customers;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.WarehouseManagement.Inventories
{
    /// <summary>
    /// 库存查询
    /// </summary>
    public class Inventory : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 库位Id
        /// </summary>
        public Guid LocationId { get; set; }
        public Location Location { get; set; }


        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public double Quantity { get; set; }


        public string Batch { get; set; }


        /// <summary>
        /// 单位价值
        /// </summary>
        public double? Price { get; set; }


        public IdentityUser Creator { get; set; }

        protected Inventory()
        {
        }

        public Inventory( Guid id ) : base(id)
        {
            
        }
    }
}
