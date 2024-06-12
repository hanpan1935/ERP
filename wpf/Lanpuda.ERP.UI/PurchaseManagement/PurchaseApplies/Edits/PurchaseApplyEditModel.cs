using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Edits
{
    public class PurchaseApplyEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

        public PurchaseApplyDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }


        public ObservableCollection<PurchaseApplyDetailEditModel> Details { get; set; }

        public PurchaseApplyEditModel()
        {
            Details = new ObservableCollection<PurchaseApplyDetailEditModel>();
        }
    }


    public class PurchaseApplyDetailEditModel : ModelBase
    {
        public Guid ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId, value); }
        }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public string ProductUnitName
        {
            get { return GetProperty(() => ProductUnitName); }
            set { SetProperty(() => ProductUnitName, value); }
        }

        public string ProductSpec
        {
            get { return GetProperty(() => ProductSpec); }
            set { SetProperty(() => ProductSpec, value); }
        }

        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }

        public DateTime? ArrivalDate
        {
            get { return GetProperty(() => ArrivalDate); }
            set { SetProperty(() => ArrivalDate, value); }
        }
    }
}
