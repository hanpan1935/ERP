using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders
{
    public class WorkOrderPagedModel : WorkOrderDto, INotifyPropertyChanged
    {
        private bool isSelected;

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
