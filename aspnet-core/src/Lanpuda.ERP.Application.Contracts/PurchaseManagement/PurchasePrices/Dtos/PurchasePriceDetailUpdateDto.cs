using System;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos;

[Serializable]
public class PurchasePriceDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid PurchasePriceId { get; set; }

    public Guid ProductId { get; set; }

    public double Price { get; set; }

    public double TaxRate { get; set; }

}