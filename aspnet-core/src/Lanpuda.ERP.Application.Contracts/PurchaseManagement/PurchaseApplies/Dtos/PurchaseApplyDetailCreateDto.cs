using System;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;

[Serializable]
public class PurchaseApplyDetailCreateDto
{

    public Guid ProductId { get; set; }

    public double Quantity { get; set; }

    public DateTime? ArrivalDate { get; set; }
}