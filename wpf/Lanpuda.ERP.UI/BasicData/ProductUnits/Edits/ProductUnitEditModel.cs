using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.BasicData.ProductUnits.Edits
{
    public class ProductUnitEditModel : ModelBase
    {
        public Guid? Id
        {
            get { return GetValue<Guid?>(nameof(Id)); }
            set { SetValue(value, nameof(Id)); }
        }


        [Required(ErrorMessage = "名称必填")]
        [MaxLength(128)]
        public  string? Name
        {
            get { return GetValue<string>(nameof(Name)); }
            set { SetValue(value, nameof(Name)); }
        }

     
        [MaxLength(128)]
        public  string? Number
        {
            get { return GetValue<string>(nameof(Number)); }
            set { SetValue(value, nameof(Number)); }
        }


        [MaxLength(128)]
        public virtual string Remark
        {
            get { return GetValue<string>(nameof(Remark)); }
            set { SetValue(value, nameof(Remark)); }
        }
    }
}
