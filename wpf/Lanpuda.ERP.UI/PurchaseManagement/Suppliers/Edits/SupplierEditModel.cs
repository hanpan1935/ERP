using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Lanpuda.ERP.PurchaseManagement.Suppliers.Edits
{
    public class SupplierEditModel : ModelBase
    {
        public Guid? Id { get; set; }


        /// <summary>
        /// 供应商编码*
        /// </summary>
        public string Number
        {
            get { return GetValue<string>(nameof(Number)); }
            set { SetValue(value, nameof(Number)); }
        }


        /// <summary>
        /// 供应商全称称*
        /// </summary>
        [Required(ErrorMessage ="必填")]
        [MaxLength(128)]
        public string FullName
        {
            get { return GetValue<string>(nameof(FullName)); }
            set { SetValue(value, nameof(FullName)); }
        }


        /// <summary>
        /// 供应商简称*
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [MaxLength(128)]
        public string ShortName
        {
            get { return GetValue<string>(nameof(ShortName)); }
            set { SetValue(value, nameof(ShortName)); }
        }



        /// <summary>
        /// 工厂地址
        /// </summary>
        [MaxLength(128)]
        public string FactoryAddress
        {
            get { return GetValue<string>(nameof(FactoryAddress)); }
            set { SetValue(value, nameof(FactoryAddress)); }
        }


        /// <summary>
        /// 主要联系人
        /// </summary>
        [MaxLength(128)]
        public string Contact
        {
            get { return GetValue<string>(nameof(Contact)); }
            set { SetValue(value, nameof(Contact)); }
        }


        /// 联系电话
        /// </summary>
        [MaxLength(128)]
        public string ContactTel
        {
            get { return GetValue<string>(nameof(ContactTel)); }
            set { SetValue(value, nameof(ContactTel)); }
        }


        #region 发票信息
        /// <summary>
        /// 单位名称
        /// </summary>
        [MaxLength(128)]
        public string OrganizationName
        {
            get { return GetValue<string>(nameof(OrganizationName)); }
            set { SetValue(value, nameof(OrganizationName)); }
        }
        /// <summary>
        ///  纳税人识别号
        /// </summary>
        [MaxLength(128)]
        public string TaxNumber
        {
            get { return GetValue<string>(nameof(TaxNumber)); }
            set { SetValue(value, nameof(TaxNumber)); }
        }
        /// <summary>
        /// 开户行
        /// </summary>
        [MaxLength(128)]
        public string BankName
        {
            get { return GetValue<string>(nameof(BankName)); }
            set { SetValue(value, nameof(BankName)); }
        }
        /// <summary>
        /// 账号
        /// </summary>
        [MaxLength(128)]
        public string AccountNumber
        {
            get { return GetValue<string>(nameof(AccountNumber)); }
            set { SetValue(value, nameof(AccountNumber)); }
        }
        /// <summary>
        /// 发票地址
        /// </summary>
        [MaxLength(128)]
        public string TaxAddress
        {
            get { return GetValue<string>(nameof(TaxAddress)); }
            set { SetValue(value, nameof(TaxAddress)); }
        }
        /// <summary>
        /// 发票电话
        /// </summary>
        [MaxLength(128)]
        public string TaxTel
        {
            get { return GetValue<string>(nameof(TaxTel)); }
            set { SetValue(value, nameof(TaxTel)); }
        }
        #endregion
    }
}
