using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.BasicData.ProductCategories.Edits
{
    public class ProductCategoryEditModel : ModelBase
    {

        public Guid? Id
        {
            get { return GetValue<Guid?>(); }
            set { SetValue(value); }
        }


        [Required(ErrorMessage = "名称必填")]
        [MaxLength(128, ErrorMessage = "最多128个字符")]
        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        [MaxLength(128, ErrorMessage = "最多128个字符")]
        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        
        [MaxLength(256, ErrorMessage = "最多256个字符")]
        public string Remark
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public ProductCategoryEditModel()
        {
            
        }

    }
}
