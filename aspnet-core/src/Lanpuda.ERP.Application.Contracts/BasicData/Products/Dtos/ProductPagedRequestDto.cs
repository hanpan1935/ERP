using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.BasicData.Products.Dtos
{
    public class ProductPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Number { get; set; }

        public Guid? ProductCategoryId { get; set; }

        public string? Name { get; set; }

        public string? Spec { get; set; }
    }
}
