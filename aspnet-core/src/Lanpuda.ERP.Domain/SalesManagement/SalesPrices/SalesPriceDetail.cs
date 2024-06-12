using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.ERP.SalesManagement.SalesPrices
{
    public class SalesPriceDetail : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 所属报价单id
        /// </summary>
        public Guid SalesPriceId { get; set; }
        public SalesPrice SalesPrice { get; set; }
    


        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        /// <summary>
        /// 销售含税价* 保留2位小数
        /// </summary>
        public double Price { get; set; }


        /// <summary>
        /// 税率* 保留2位小数
        /// </summary>
        public double TaxRate { get; set; }


        public int Sort { get; set; }

        protected SalesPriceDetail()
        {
        }

        public SalesPriceDetail(Guid id) : base(id)
        {
          
        }
    }
}
