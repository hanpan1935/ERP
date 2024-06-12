using System;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Dtos;

[Serializable]
public class PurchaseReturnDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid LocationId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }

}