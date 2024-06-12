using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.SalesManagement.SalesPrices.Dtos;

[Serializable]
public class SalesPriceUpdateDto
{
    public string Number { get; set; }

    public Guid CustomerId { get; set; }

    public DateTime ValidDate { get; set; }

    public string Remark { get; set; }

    public List<SalesPriceDetailUpdateDto> Details { get; set; }

    public SalesPriceUpdateDto()
    {
        Details = new List<SalesPriceDetailUpdateDto>();
    }
}