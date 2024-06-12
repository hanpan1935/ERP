using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.BasicData.Products.Dtos
{
    public class ProductWithPriceDto : EntityDto<Guid>
    {
        public string Number { get; set; }

        public Guid? ProductCategoryId { get; set; }

        public string ProductCategoryName { get; set; }

        public Guid ProductUnitId { get; set; }

        public string ProductUnitName { get; set; }

        public string Name { get; set; }

        public string Spec { get; set; }

        public ProductSourceType SourceType { get; set; }

        public double? Price { get; set; }

        public double? TaxRate { get; set; }

    }
}
