using System;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;

[Serializable]
public class PurchaseReturnApplyDetailCreateDto
{

    public Guid PurchaseStorageDetailId { get; set; }


    public double Quantity { get; set; }


    public string Remark { get; set; }

}