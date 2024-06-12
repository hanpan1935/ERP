using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices
{
    public class ArrivalNoticePagedRequestModel : ModelBase
    {
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }


        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }


        public bool? IsInspect
        {
            get { return GetProperty(() => IsInspect); }
            set { SetProperty(() => IsInspect, value); }
        }

        public DateTime? ArrivalTimeStart
        {
            get { return GetProperty(() => ArrivalTimeStart); }
            set { SetProperty(() => ArrivalTimeStart, value); }
        }


        public DateTime? ArrivalTimeEnd
        {
            get { return GetProperty(() => ArrivalTimeEnd); }
            set { SetProperty(() => ArrivalTimeEnd, value); }
        }

        public Dictionary<string, bool> IsConfirmedSource { get; set; }

        public Dictionary<string, bool> IsInspectSource { get; set; }

        public ArrivalNoticePagedRequestModel()
        {
            IsConfirmedSource = ItemsSoureUtils.GetBoolDictionary();
            IsInspectSource = ItemsSoureUtils.GetBoolDictionary();
        }
    }
}
