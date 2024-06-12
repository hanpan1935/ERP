using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lanpuda.ERP.UI.ProductionManagement.Mpses.Profiles.Converts
{
    [ValueConversion(typeof(MrpDetailProductProfileModel), typeof(String))]
    public class ProductToButtonContentConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MrpDetailProductProfileModel product = (MrpDetailProductProfileModel)value;

            if (product.ProductSourceType == ProductSourceType.Self)
            {
                return "创建生产工单";
            }
            else if (product.ProductSourceType == ProductSourceType.Purchase)
            {
                return "创建采购申请";
            }
            else if (product.ProductSourceType == ProductSourceType.Outsourcing)  //委外
            {
                return "创建采购申请";
            }
            else if (product.ProductSourceType == ProductSourceType.Customer)    //客供
            {
                return "创建采购申请";
            }
            else if (product.ProductSourceType == ProductSourceType.Fictitious) //虚拟
            {
                return "创建采购申请";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
