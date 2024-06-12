using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.WarehouseManagement.OtherOuts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.BasicData.Products;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages
{
    public class OtherStorageDetail : AuditedAggregateRoot<Guid>
    {
        public Guid OtherStorageId { get; set; }
        public OtherStorage OtherStorage { get; set; }


        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public Guid LocationId { get; set; }
        public Location Location { get; set; }


        [MaxLength(128)]
        public string Batch { get; set; }


        /// <summary>
        /// 入库数量
        /// </summary>
        [Required]
        public double Quantity { get; set; }

        public double Price { get; set; }

        public int Sort { get; set; }
        protected OtherStorageDetail()
        {
        }

        public OtherStorageDetail(Guid id) : base(id)
        {
        }
    }
}
