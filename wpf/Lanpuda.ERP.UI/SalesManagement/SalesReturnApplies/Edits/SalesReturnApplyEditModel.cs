using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using Lanpuda.Client.Common;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies.Edits
{
    public class SalesReturnApplyEditModel : ModelBase
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

        public Guid CustomerId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value, OnCustomerIdChanged); }
        }


        public string CustomerShortName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public string CustomerFullName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 退货原因
        /// </summary>
        public SalesReturnReason Reason
        {
            get { return GetValue<SalesReturnReason>(); }
            set { SetValue(value); }
        }


        /// <summary>
        /// 商品是否要退回
        /// 如果否 不创建退货入库单, 但是创建应收明细
        /// </summary>
        public bool IsProductReturn
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 具体描述
        /// </summary>
        [MaxLength(256)]
        public string Description
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<SalesReturnApplyDetailEditModel> Details { get; set; }

        public SalesReturnApplyEditModel()
        {
            Details = new ObservableCollection<SalesReturnApplyDetailEditModel>();

            CustomerSource = new ObservableCollection<CustomerLookupDto>();
            ReturnReasonSource = EnumUtils.EnumToDictionary<SalesReturnReason>();
            IsProductReturnSource = new Dictionary<string, bool>
            {
                { "是", true },
                { "否", false }
            };
        }


        public ObservableCollection<CustomerLookupDto> CustomerSource { get; set; }
        public Dictionary<string, SalesReturnReason> ReturnReasonSource { get; set; }
        public Dictionary<string, bool> IsProductReturnSource { get; set; }


        private void OnCustomerIdChanged()
        {
            if (this.CustomerId != Guid.Empty)
            {
                var customer = this.CustomerSource.Where(m => m.Id == this.CustomerId).First();
                this.CustomerFullName = customer.FullName;
                this.CustomerShortName = customer.ShortName;
            }
        }

    }


    public class SalesReturnApplyDetailEditModel : ModelBase
    {
        public Guid SalesOutDetailId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }
        public string ProductName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Batch
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        /// <summary>
        /// 退货数量
        /// </summary>
        public double Quantity
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }


        public string Description
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 出库数量
        /// </summary>
        public double OutQuantity
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }
       

        

        public string ProductNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ProductUnitName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ProductSpec
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
   
    }
}
