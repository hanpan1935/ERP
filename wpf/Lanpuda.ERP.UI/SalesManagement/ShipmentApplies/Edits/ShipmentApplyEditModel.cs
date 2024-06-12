using HandyControl.Collections;
using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies.Edits
{
    public class ShipmentApplyEditModel : ModelBase
    {
        public Guid? Id
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }

        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        [GuidNotEmpty]
        public Guid CustomerId
        {
            get { return GetProperty(() => CustomerId); }
            set { SetProperty(() => CustomerId, value, OnCustomerIdChanged); }
        }

        public string CustomerName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string CustomerNameSearchText
        {
            get { return GetValue<string>(); }
            set { SetValue(value, OnCustomerNameSearchTextChanged); }
        }


        /// <summary>
        /// 收货地址
        /// </summary>
        public string ShippingAddress
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ConsigneeTel
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public ShipmentApplyDetailEditModel? SelectedRow { get; set; }

        public ObservableCollection<ShipmentApplyDetailEditModel> Details { get; set; }


        public List<CustomerLookupDto> customerList;
        public ManualObservableCollection<CustomerLookupDto> CustomerSource
        {
            get { return GetProperty(() => CustomerSource); }
            set { SetProperty(() => CustomerSource, value); }
        }

        public ShipmentApplyEditModel()
        {
            Details = new ObservableCollection<ShipmentApplyDetailEditModel>();
            customerList = new List<CustomerLookupDto>();
            CustomerSource = new ManualObservableCollection<CustomerLookupDto>();
        }

        private void OnCustomerNameSearchTextChanged()
        {
            List<CustomerLookupDto> list = new List<CustomerLookupDto>();  
            if (string.IsNullOrEmpty(this.CustomerNameSearchText))
            {
                list = this.customerList;
            }
            else
            {
                list = this.customerList.Where(m => m.FullName.Contains(this.CustomerNameSearchText)).ToList();
            }
            CustomerSource.CanNotify = false;
            CustomerSource.Clear();
            foreach (var item in list)
            {
                CustomerSource.Add(item);
            }
            CustomerSource.CanNotify = true;
        }


        private void OnCustomerIdChanged()
        {
            if (this.CustomerId != Guid.Empty)
            {
                var customer = this.customerList.Where(m => m.Id == this.CustomerId).First();
                this.Consignee = customer.Consignee;
                this.ConsigneeTel = customer.ConsigneeTel;
                this.ShippingAddress = customer.ShippingAddress;
                this.CustomerName = customer.FullName;
            }
        }
    }

    public class ShipmentApplyDetailEditModel : ModelBase
    {
        public Guid SalesOrderDetailId { get; set; }

        public string SalesOrderNumber
        {
            get { return GetProperty(() => SalesOrderNumber); }
            set { SetProperty(() => SalesOrderNumber, value); }
        }

        /// <summary>
        /// 申请数量
        /// </summary>
        public double Quantity
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 订单数量
        /// </summary>
        public double OrderQuantity
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }
        

        public DateTime DeliveryDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }


        public string ProductName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ProductNumber
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

        public string Requirement
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
