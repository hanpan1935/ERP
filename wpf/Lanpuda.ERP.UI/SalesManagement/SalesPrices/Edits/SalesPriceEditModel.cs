using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.ERP.Permissions.ERPPermissions;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.Client.Common.Attributes;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesPrices.Edits
{
    public class SalesPriceEditModel : ModelBase
    {
        public Guid? Id
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 报价单号
        /// </summary>
        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        //[GuidNotEmpty(ErrorMessage = "必选")]
        //public Guid CustomerId
        //{
        //    get { return GetValue<Guid>(); }
        //    set { SetValue(value); }
        //}


        [Required(ErrorMessage = "必选")]
        public CustomerLookupDto? Customer
        {
            get { return GetProperty(() => Customer); }
            set { SetProperty(() => Customer, value); }
        }

        /// <summary>
        /// 有效期
        /// </summary>
        /// 
        [Column(TypeName = "date")]
        public DateTime ValidDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }


        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public string Remark
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public SalesPriceDetailEditModel? SelectedDetailEntity { get; set; }

        public ObservableCollection<SalesPriceDetailEditModel> Details { get; set; }

        public SalesPriceEditModel()
        {
            Details = new ObservableCollection<SalesPriceDetailEditModel>();
        }
    }

    public class SalesPriceDetailEditModel : ModelBase
    {

        public Guid ProductId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }

        public string ProductNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ProductName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ProductSpec
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public string ProductUnitName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        /// <summary>
        /// 销售含税价* 保留2位小数
        /// </summary>
        public double Price
        {
            get { return GetValue<double>(); }
            set { SetValue(value, NotifyNetPriceChanged); }
        }


        /// <summary>
        /// 税率*
        /// </summary>
        public double TaxRate
        {
            get { return GetValue<double>(); }
            set { SetValue(value, NotifyNetPriceChanged); }
        }


        public string NetPrice
        {
            get
            {
                double result = this.Price - (this.Price * (this.TaxRate / 100));
                return result.ToString();
            }
            set { SetValue(value); }
        }

        public SalesPriceDetailEditModel()
        {
        }

        void NotifyNetPriceChanged()
        {
            RaisePropertyChanged(nameof(NetPrice));
        }
    }
}
