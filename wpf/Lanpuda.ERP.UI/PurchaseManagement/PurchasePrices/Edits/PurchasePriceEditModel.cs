using Lanpuda.Client.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices.Edits
{
    public class PurchasePriceEditModel : ModelBase
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


        /// <summary>
        /// 客户Id
        /// </summary>
        [GuidNotEmpty(ErrorMessage = "供应商必选")]
        public Guid SupplierId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }




        /// <summary>
        /// 客户经理Id
        /// </summary>
        [Required]
        public int AvgDeliveryDate
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 有效期
        /// </summary>
        /// 
        [Column(TypeName = "date")]
        public DateTime QuotationDate
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


        public PurchasePriceDetailEditModel? SelectedRow
        {
            get { return GetValue<PurchasePriceDetailEditModel?>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<PurchasePriceDetailEditModel> Details { get; set; }



        public ObservableCollection<SupplierDto> SupplierSource
        {
            get { return GetValue<ObservableCollection<SupplierDto>>(); }
            set { SetValue(value); }
        }


        public PurchasePriceEditModel()
        {
            Details = new ObservableCollection<PurchasePriceDetailEditModel>();
            SupplierSource = new ObservableCollection<SupplierDto>();
        }

    }


    public class PurchasePriceDetailEditModel : ModelBase
    {

        public Guid? Id { get; set; }



        [GuidNotEmpty(ErrorMessage = "产品必选")]
        public Guid ProductId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }

        public string ProductNumner
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
        /// 
        [Required(ErrorMessage = "必填")]
        public double Price
        {
            get { return GetValue<double>(); }
            set { SetValue(value, NotifyNetPriceChanged); }
        }

        /// <summary>
        /// 税率* 保留2位小数
        /// </summary>
        [Required(ErrorMessage = "必填")]
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

        public PurchasePriceDetailEditModel()
        {
           
        }


        void NotifyNetPriceChanged()
        {
            RaisePropertyChanged(nameof(NetPrice));
        }

    }
}
