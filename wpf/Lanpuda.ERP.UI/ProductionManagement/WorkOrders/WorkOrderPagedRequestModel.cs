using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders
{
    public class WorkOrderPagedRequestModel : ModelBase
    {

        public string Sorting
        {
            get { return GetProperty(() => Sorting); }
            set { SetProperty(() => Sorting, value); }
        }
        
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string MpsNumber
        {
            get { return GetProperty(() => MpsNumber); }
            set { SetProperty(() => MpsNumber, value); }
        }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }
        /// <summary>
        /// 开工时间
        /// </summary>
        public DateTime? StartDate
        {
            get { return GetProperty(() => StartDate); }
            set { SetProperty(() => StartDate, value); }
        }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompletionDate
        {
            get { return GetProperty(() => CompletionDate); }
            set { SetProperty(() => CompletionDate, value); }
        }

        public Dictionary<string,bool> IsConfirmedSource { get; set; }

        public WorkOrderPagedRequestModel()
        {
            IsConfirmedSource = ItemsSoureUtils.GetBoolDictionary();

        }
    }
}
