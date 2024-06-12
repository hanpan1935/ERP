using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.BasicData.ProductCategories.Dtos
{
    public class ProductCategoryPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Name { get; set; }
    }
}
