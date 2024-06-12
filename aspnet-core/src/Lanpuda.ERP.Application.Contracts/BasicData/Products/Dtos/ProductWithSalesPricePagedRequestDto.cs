using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.BasicData.Products.Dtos
{
    public class ProductWithSalesPricePagedRequestDto : ProductPagedRequestDto
    {
        [Required]
        public Guid CustomerId { get; set; }

    }
}
