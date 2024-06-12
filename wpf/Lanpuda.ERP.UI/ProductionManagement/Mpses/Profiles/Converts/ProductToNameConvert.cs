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
    public class ProductToNameConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MrpDetailProductProfileModel product = (MrpDetailProductProfileModel)value;
            return product.ProductName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
