using HandyControl.Collections;
using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Utils;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders
{
    public class PurchaseOrderPagedRequestModel : ModelBase
    {
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public Guid? SupplierId
        {
            get { return GetProperty(() => SupplierId); }
            set { SetProperty(() => SupplierId, value); }
        }

        public string SupplierSearchText
        {
            get { return GetProperty(() => SupplierSearchText); }
            set { SetProperty(() => SupplierSearchText, value, FilterItems); }
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


        public  List<SupplierDto> _dataList { get; set; }


        public ManualObservableCollection<SupplierDto> SupplierSource
        {
            get { return GetProperty(() => SupplierSource); }
            set { SetProperty(() => SupplierSource, value); }
        }



        public PurchaseOrderType? OrderType
        {
            get { return GetProperty(() => OrderType); }
            set { SetProperty(() => OrderType, value); }
        }

        public PurchaseOrderCloseStatus? CloseStatus
        {
            get { return GetProperty(() => CloseStatus); }
            set { SetProperty(() => CloseStatus, value); }
        }

        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }


        public Dictionary<string, PurchaseOrderType> OrderTypeSource { get; set; }
        public Dictionary<string, PurchaseOrderCloseStatus> CloseStatusSource { get; set; }
        public Dictionary<string,bool> IsConfirmedSource { get; set; }



        public PurchaseOrderPagedRequestModel()
        {
            _dataList = new List<SupplierDto>();
            SupplierSource = new ManualObservableCollection<SupplierDto>();
            OrderTypeSource = EnumUtils.EnumToDictionary<PurchaseOrderType>();
            CloseStatusSource = EnumUtils.EnumToDictionary<PurchaseOrderCloseStatus>();
            IsConfirmedSource = ItemsSoureUtils.GetBoolDictionary();
        }


        private void FilterItems()
        {
            string key = this.SupplierSearchText;
            SupplierSource.CanNotify = false;
            SupplierSource.Clear();

            if (string.IsNullOrEmpty(key))
            {
                foreach (var item in _dataList)
                {
                    SupplierSource.Add(item);
                }
            }
            else
            {
                var items = _dataList.Where( m=>m .FullName.Contains(key));
                foreach (var item in items)
                {
                    SupplierSource.Add(item);
                } 
            }
            SupplierSource.CanNotify = true;
        }
    }
}
