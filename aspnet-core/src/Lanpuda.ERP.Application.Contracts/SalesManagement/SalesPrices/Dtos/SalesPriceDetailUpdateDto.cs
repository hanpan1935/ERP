using System;

namespace Lanpuda.ERP.SalesManagement.SalesPrices.Dtos;

[Serializable]
public class SalesPriceDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid SalesPriceId { get; set; }

    public Guid ProductId { get; set; }

    public double Price { get; set; }

    public double TaxRate { get; set; }

    public int Sort { get; set; }
}