using System;

namespace Lanpuda.ERP.SalesManagement.SalesPrices.Dtos;

[Serializable]
public class SalesPriceDetailCreateDto
{
    public Guid ProductId { get; set; }

    public double Price { get; set; }

    public double TaxRate { get; set; }

    public int Sort { get; set; }
}