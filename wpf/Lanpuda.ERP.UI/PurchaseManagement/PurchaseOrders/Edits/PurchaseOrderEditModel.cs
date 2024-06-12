using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Edits
{
    public class PurchaseOrderEditModel : ModelBase
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
            set { SetValue(value, OnSuppliIdChanged); }
        }

        public string SupplierFullName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string SupplierShortName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 要求交期
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public DateTime RequiredDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 承诺交期
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? PromisedDate
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }


        /// <summary>
        /// 收货人
        /// </summary>
        [MaxLength(256)]
        public string Contact
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 收货人电话
        /// </summary>
        [MaxLength(256)]
        public string ContactTel
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 送货地址
        /// </summary>
        [MaxLength(256)]
        public string ShippingAddress
        {
            get { return GetValue<string>(); }
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

        public ObservableCollection<SupplierDto> SupplierSource
        {
            get { return GetValue<ObservableCollection<SupplierDto>>(); }
            set { SetValue(value); }
        }

        public PurchaseOrderDetailEditModel? SelectedRow
        {
            get { return GetValue<PurchaseOrderDetailEditModel>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<PurchaseOrderDetailEditModel> Details { get; set; }

        public PurchaseOrderEditModel()
        {
            Details = new ObservableCollection<PurchaseOrderDetailEditModel>();
            SupplierSource = new ObservableCollection<SupplierDto>();
        }

        void OnSuppliIdChanged()
        {
            var supplier = SupplierSource.Where(m => m.Id == SupplierId).First();
            this.SupplierFullName = supplier.FullName;
            this.SupplierShortName = supplier.ShortName;
        }
    }


    public class PurchaseOrderDetailEditModel : ModelBase
    {

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "必填")]
        public DateTime PromiseDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        [GuidNotEmpty(ErrorMessage = "必填")]
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
        /// 采购数量
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [StringCanToDouble(ErrorMessage = "请输入数字")]
        public double Quantity
        {
            get { return GetValue<double>(); }
            set { SetValue(value, NotifyPriceChanged); }
        }


        /// <summary>
        /// 销售含税价* 保留2位小数
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [StringCanToDouble(ErrorMessage = "请输入数字")]
        public double Price
        {
            get { return GetValue<double>(); }
            set { SetValue(value, NotifyPriceChanged); }
        }



        /// <summary>
        /// 税率* 保留2位小数
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [StringCanToDouble(ErrorMessage = "请输入数字")]
        public double TaxRate
        {
            get { return GetValue<double>(); }
            set { SetValue(value, NotifyPriceChanged); }
        }

        public double NetPrice
        {
            get
            {
                double result = Price - (Price * (TaxRate / 100));
                return result;
            }
            set { SetValue(value); }
        }


        public double TotalPrice
        {
            get
            {
                //double price = 0;
                //bool isPrice = double.TryParse(Price, out price);

                ////double taxRate = 0;
                ////bool isTaxRate = double.TryParse(TaxRate, out taxRate);

                //double quantity = 0;
                //bool isQuantity = double.TryParse(Quantity, out quantity);

                //if (isPrice != true || isQuantity != true)
                //{
                //    return "";
                //}
                double result = Price * Quantity;
                return result;
            }
            set { SetValue(value); }
        }


        [MaxLength(128)]
        public string Remark
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public PurchaseOrderDetailEditModel()
        {
        }


        void NotifyPriceChanged()
        {
            RaisePropertyChanged(nameof(TotalPrice));
            RaisePropertyChanged(nameof(NetPrice));
        }

    }
}
