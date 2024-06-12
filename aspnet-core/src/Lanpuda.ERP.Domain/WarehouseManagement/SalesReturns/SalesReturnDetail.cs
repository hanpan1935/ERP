using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.BasicData.Products;

namespace Lanpuda.ERP.WarehouseManagement.SalesReturns
{
    public class SalesReturnDetail : AuditedAggregateRoot<Guid>
    {
        public Guid SalesReturnId { get; set; }
        public SalesReturn SalesReturn { get; set; }


        /// <summary>
        /// 冗余数据
        /// </summary>
        //public Guid ProductId { get; set; }
        //public Product Product { get; set; }



        public Guid LocationId { get; set; }
        public Location Location { get; set; }


        [MaxLength(128)]
        public string Batch { get; set; }


        /// <summary>
        /// 入库数量
        /// </summary>
        public double Quantity { get; set; }


        public int Sort { get; set; }
        protected SalesReturnDetail()
        {
        }

        public SalesReturnDetail(Guid id) : base(id)
        {
        }
    }
}
