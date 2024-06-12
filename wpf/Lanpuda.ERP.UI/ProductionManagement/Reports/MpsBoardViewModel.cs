using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.ProductionManagement.Reports
{
    public class MpsBoardViewModel : RootViewModelBase
    {

        public string Name
        {
            get { return GetProperty(() => Name); }
            private set { SetProperty(() => Name, value); }
        }

    }
}
