using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.SalesManagement.SalesPrices.Dtos;

[Serializable]
public class SalesPriceCreateDto
{
    public Guid CustomerId { get; set; }

    public DateTime ValidDate { get; set; }

    public string Remark { get; set; }

    public List<SalesPriceDetailCreateDto> Details { get; set; }

    public SalesPriceCreateDto()
    {
        Details = new List<SalesPriceDetailCreateDto>();
    }
}