using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.ProductionManagement.Workshops.Edits
{
    public class WorkshopEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "必填")]
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        [Required(ErrorMessage = "必填")]
        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }


        public string Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }
    }
}
