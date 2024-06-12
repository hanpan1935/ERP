using Lanpuda.Client.Common;
using Lanpuda.ERP.BasicData.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.BasicData.Products
{
    public class ProductPagedModel : ProductDto
    {
        public string SourceTypeStr
        {
            get
            {
                return EnumUtils.GetEnumDisplayName(this.SourceType);
            }
        }
    }
}
