using System;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos;

[Serializable]
public class PurchasePriceDetailCreateDto
{
    public Guid ProductId { get; set; }

    public double Price { get; set; }

    public double TaxRate { get; set; }

}