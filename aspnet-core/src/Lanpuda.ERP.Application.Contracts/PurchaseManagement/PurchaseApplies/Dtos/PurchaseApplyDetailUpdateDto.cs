using System;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;

[Serializable]
public class PurchaseApplyDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid ProductId { get; set; }

    public double Quantity { get; set; }

    public DateTime? ArrivalDate { get; set; }
}