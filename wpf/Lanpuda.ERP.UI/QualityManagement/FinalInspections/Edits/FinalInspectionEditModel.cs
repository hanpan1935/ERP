using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.QualityManagement.FinalInspections.Edits
{
    public class FinalInspectionEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        /// <summary>
        /// 不良数量
        /// </summary>
        public double BadQuantity
        {
            get { return GetProperty(() => BadQuantity); }
            set { SetProperty(() => BadQuantity, value); }
        }

        /// <summary>
        /// 情况描述
        /// </summary>
        public string Description
        {
            get { return GetProperty(() => Description); }
            set { SetProperty(() => Description, value); }
        }
    }
}
