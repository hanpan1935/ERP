using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.ERP.SalesManagement.Customers;
using Volo.Abp.Identity;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.SalesManagement.ShipmentApplies;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies;
using Lanpuda.ERP.WarehouseManagement.SalesOuts;
using Lanpuda.ERP.WarehouseManagement.SalesReturns;
using Lanpuda.ERP.ProductionManagement.Mpses;

namespace Lanpuda.ERP.SalesManagement.SalesOrders
{
    public class SalesOrderDetail : AuditedAggregateRoot<Guid>
    {
        public Guid SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }


        /// <summary>
        /// 交货日期*
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime DeliveryDate { get; set; }


        /// <summary>
        /// 订单数量*
        /// </summary>
        public double Quantity { get; set; }


        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        /// <summary>
        /// 含税价格*  保留2位小数
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 税率* 保留2位小数
        /// </summary>
        public double TaxRate { get; set; }

        /// <summary>
        /// 特殊要求
        /// </summary>
        [MaxLength(128, ErrorMessage = "128")]
        public string Requirement { get; set; }


        public List<Mps> MpsList { get; set; }

        public int Sort { get; set; }


        protected SalesOrderDetail()
        {
        }

        public SalesOrderDetail(Guid id) : base(id)
        {
          
        }
    }
}
