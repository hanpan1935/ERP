using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.SalesManagement.Customers
{
    public class Customer : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 客户编码*
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Number { get; set; }

        /// <summary>
        /// 客户名称*
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string FullName { get; set; }

        /// <summary>
        /// 客户简称*
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string ShortName { get; set; }
        
        /// <summary>
        /// 联系人
        /// </summary>
        [MaxLength(128)]
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(128)]
        public string ContactTel { get; set; }



        /// <summary>
        /// 收货地址
        /// </summary>
        [MaxLength(256)]
        public string ShippingAddress { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        [MaxLength(256)]
        public string Consignee { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        [MaxLength(256)]
        public string ConsigneeTel { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [MaxLength(256)]
        public string OrganizationName { get; set; }

        /// <summary>
        ///  纳税人识别号
        /// </summary>
        [MaxLength(256)]
        public string TaxNumber { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        [MaxLength(256)]
        public string BankName { get; set; }

        /// <summary>
        /// 开户行账号
        /// </summary>
        [MaxLength(256)]
        public string AccountNumber { get; set; }

        /// <summary>
        /// 发票地址
        /// </summary>
        [MaxLength(256)]
        public string TaxAddress { get; set; }

        /// <summary>
        /// 发票电话
        /// </summary>
        [MaxLength(256)]
        public string TaxTel { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public string Description { get; set; }


        public IdentityUser Creator { get; set; }

        protected Customer()
        {
        }

        public Customer( Guid id) : base(id)
        {
        }
    }
}
