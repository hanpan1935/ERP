using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.SalesManagement.SalesOrders
{
    public class SalesOrderPagedRequestModel : ModelBase
    {
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public Guid? CustomerId
        {
            get { return GetProperty(() => CustomerId); }
            set { SetProperty(() => CustomerId, value); }
        }

        public string CustomerName
        {
            get { return GetProperty(() => CustomerName); }
            set { SetProperty(() => CustomerName, value); }
        }

        public DateTime? RequiredDateStart
        {
            get { return GetProperty(() => RequiredDateStart); }
            set { SetProperty(() => RequiredDateStart, value); }
        }
        public DateTime? RequiredDateEnd
        {
            get { return GetProperty(() => RequiredDateEnd); }
            set { SetProperty(() => RequiredDateEnd, value); }
        }


        public DateTime? PromisedDateStart
        {
            get { return GetProperty(() => PromisedDateStart); }
            set { SetProperty(() => PromisedDateStart, value); }
        }
        public DateTime? PromisedDateEnd
        {
            get { return GetProperty(() => PromisedDateEnd); }
            set { SetProperty(() => PromisedDateEnd, value); }
        }


        public SalesOrderType? OrderType
        {
            get { return GetProperty(() => OrderType); }
            set { SetProperty(() => OrderType, value); }
        }

        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }

        public SalesOrderCloseStatus? CloseStatus
        {
            get { return GetProperty(() => CloseStatus); }
            set { SetProperty(() => CloseStatus, value); }
        }

        public Dictionary<string, SalesOrderType> SalesOrderTypeSource { get; set; }
        public Dictionary<string, SalesOrderCloseStatus> CloseStatusSource { get; set; }
        public Dictionary<string, bool> IsConfirmedSource { get; set; }

        public SalesOrderPagedRequestModel()
        {
            SalesOrderTypeSource = EnumUtils.EnumToDictionary<SalesOrderType>();
            CloseStatusSource = EnumUtils.EnumToDictionary<SalesOrderCloseStatus>();
            IsConfirmedSource = new Dictionary<string, bool>();
            IsConfirmedSource.Add("是", true);
            IsConfirmedSource.Add("否", false);
        }
    }
}
