using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.ERP.BasicData.Products;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.WarehouseManagement.Inventories
{
    /// <summary>
    /// 库存流水
    /// </summary>
    public class InventoryLog : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 发生单号
        /// </summary>
        public string Number { get; set; }


        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public Guid LocationId { get; set; }
        public Location Location { get; set; }


        /// <summary>
        ///  出入时间
        /// </summary>
        public DateTime LogTime { get; set; }


        /// <summary>
        /// 发生类型
        /// </summary>
        public InventoryLogType LogType { get; set; }



        /// <summary>
        /// 批次号
        /// </summary>
        public string Batch { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        public double InQuantity { get; set; }


        /// <summary>
        /// 出库数量
        /// </summary>
        public double OutQuantity { get; set; }


        /// <summary>
        /// 发生后数量
        /// </summary>
        public double AfterQuantity { get; set; }


        public IdentityUser Creator { get; set; }

        protected InventoryLog()
        {
        }

        public InventoryLog( Guid id) : base(id)
        {
        }
    }
}
