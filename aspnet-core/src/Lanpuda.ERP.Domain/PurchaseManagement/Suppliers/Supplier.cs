using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.PurchaseManagement.Suppliers
{
    public class Supplier : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 供应商名称*
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string FullName { get; set; }

        /// <summary>
        /// 供应商简称*
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string ShortName { get; set; }

     
        /// <summary>
        /// 工厂地址
        /// </summary>
        public string FactoryAddress { get; set; }

        /// <summary>
        /// 主要联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }


        #region 发票信息
        /// <summary>
        /// 单位名称
        /// </summary>
        public string OrganizationName { get; set; }
        /// <summary>
        ///  纳税人识别号
        /// </summary>
        public string TaxNumber { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// 发票地址
        /// </summary>
        [MaxLength(128)]
        public string TaxAddress { get; set; }
        /// <summary>
        /// 发票电话
        /// </summary>
        public string TaxTel { get; set; }

        #endregion


        public IdentityUser Creator { get; set; }
        protected Supplier()
        {
        }

        public Supplier(Guid id) : base(id)
        {
          
        }
    }
}
