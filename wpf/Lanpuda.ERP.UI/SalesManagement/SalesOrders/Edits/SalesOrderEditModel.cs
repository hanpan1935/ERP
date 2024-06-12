using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Edits
{
    public class SalesOrderEditModel : ModelBase
    {
        public Guid? Id
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 订单编号*
        /// </summary>
        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        /// <summary>
        /// 客户Id*
        /// </summary>
        [GuidNotEmpty(ErrorMessage = "客户必选")]
        public Guid CustomerId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 要求交期*
        /// </summary>
        public DateTime? RequiredDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 承诺交期*
        /// </summary>
        public DateTime? PromisedDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 订单类型
        /// </summary>
        public SalesOrderType OrderType
        {
            get { return GetValue<SalesOrderType>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 订单说明
        /// </summary>
        [MaxLength(256, ErrorMessage = "最长256个字符")]
        public string Description
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public SalesOrderDetailEditModel? SelectedDetailEntity { get; set; }


        /// <summary>
        /// 订单明细
        /// </summary>
        public ObservableCollection<SalesOrderDetailEditModel> Details { get; set; }


        public SalesOrderEditModel()
        {
            Details = new ObservableCollection<SalesOrderDetailEditModel>();
        }

    }


    public class SalesOrderDetailEditModel : ModelBase
    {
        /// <summary>
        /// 交货日期*
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime DeliveryDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }
        /// <summary>
        /// 订单数量*
        /// </summary>
        public double Quantity
        {
            get { return GetValue<double>(); }
            set { SetValue(value, NotifyTotalPriceChanged); }
        }


        /// <summary>
        /// 产品Id
        /// </summary>
        [GuidNotEmpty(ErrorMessage = "产品必选")]
        public Guid ProductId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }

        public string ProductName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ProductNumber
        {
            get { return GetProperty(() => ProductNumber); }
            set { SetProperty(() => ProductNumber, value); }
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
        /// 含税价格*  保留2位小数
        /// </summary>
        public double Price
        {
            get { return GetValue<double>(); }
            set { SetValue(value, NotifyTotalPriceChanged); }
        }

        /// <summary>
        /// 税率* 保留2位小数
        /// </summary>
        public double TaxRate
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 特殊要求
        /// </summary>
        [MaxLength(128, ErrorMessage = "128")]
        public string Requirement
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public double TotalPrice 
        { 
            get
            {
                return this.Price * this.Quantity;
            } 
        }

        public SalesOrderDetailEditModel()
        {
        }


        void NotifyTotalPriceChanged()
        {
            RaisePropertyChanged(nameof(TotalPrice));
        }
    }
}
