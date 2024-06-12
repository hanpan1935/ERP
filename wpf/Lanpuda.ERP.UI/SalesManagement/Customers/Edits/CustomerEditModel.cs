using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.SalesManagement.Customers.Edits
{
    public class CustomerEditModel : ModelBase
    {
        public Guid? Id
        {
            get { return GetValue<Guid?>(nameof(Id)); }
            set { SetValue(value, nameof(Id)); }
        }

        [Required(ErrorMessage ="客户编码必填")]
        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        [Required(ErrorMessage = "客户全称必填")]
        public string FullName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        [Required(ErrorMessage = "客户简称必填")]
        public string ShortName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Contact
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public string ContactTel
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 收货地址
        /// </summary>
        [MaxLength(256)]
        public string ShippingAddress
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        /// <summary>
        /// 收货人
        /// </summary>
        [MaxLength(256)]
        public string Consignee
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 收货人联系电话
        /// </summary>
        [MaxLength(256)]
        public string ConsigneeTel
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        /// <summary>
        /// 单位名称
        /// </summary>
        [MaxLength(256)]
        public string OrganizationName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        ///  纳税人识别号
        /// </summary>
        [MaxLength(256)]
        public string TaxNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 开户行
        /// </summary>
        [MaxLength(256)]
        public string BankName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        /// <summary>
        /// 账号
        /// </summary>
        [MaxLength(256)]
        public string AccountNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 发票地址
        /// </summary>
        [MaxLength(256)]
        public string TaxAddress
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        /// <summary>
        /// 发票电话
        /// </summary>
        [MaxLength(256)]
        public string TaxTel
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public string Description
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public CustomerEditModel()
        {
        }

    }
}
