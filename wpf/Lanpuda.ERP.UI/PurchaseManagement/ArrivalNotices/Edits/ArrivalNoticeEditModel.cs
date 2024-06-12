using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Edits
{

    public class ArrivalNoticeEditModel : ModelBase
    {
        #region Model

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


        /// <summary>
        /// 来料时间
        /// </summary>
        public DateTime ArrivalTime
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }


        public string Remark
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public ArrivalNoticeDetailEditModel? SelectedRow
        {
            get { return GetValue<ArrivalNoticeDetailEditModel?>(); }
            set { SetValue(value); }
        }


        public ObservableCollection<ArrivalNoticeDetailEditModel> Details
        {
            get { return GetValue<ObservableCollection<ArrivalNoticeDetailEditModel>>(); }
            set { SetValue(value); }
        }

        #endregion
    }


    public class ArrivalNoticeDetailEditModel : ModelBase
    {
        public Guid? Id
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }

        public string PurchaseOrderNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public double PurchaseOrderDetailQuantity
        {
            get { return GetValue<double>(); }
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


        public Guid PurchaseOrderDetailId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 来料数量
        /// </summary>
        public double Quantity
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

    }
}
