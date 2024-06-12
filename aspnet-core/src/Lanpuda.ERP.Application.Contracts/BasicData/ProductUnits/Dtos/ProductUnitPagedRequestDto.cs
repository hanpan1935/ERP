using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.BasicData.ProductUnits.Dtos
{
    public class ProductUnitPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Name { get; set; }
    }
}
