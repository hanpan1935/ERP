using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;

[Serializable]
public class PurchaseReturnApplyDetailDto : EntityDto<Guid>
{
    public Guid PurchaseReturnApplyId { get; set; }

    public Guid PurchaseStorageDetailId { get; set; }

    public string PurchaseStorageNumber { get; set; }

    public double Quantity { get; set; }

    public string Batch { get; set; }

    public string Remark { get; set; }

    public Guid ProductId { get; set; }

    public string ProductNumber { get; set; }

    public string ProductName { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }
}