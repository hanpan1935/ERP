using System;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;

[Serializable]
public class PurchaseReturnApplyDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid PurchaseStorageDetailId { get; set; }

    public double Quantity { get; set; }

    public string Remark { get; set; }
}