using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;

[Serializable]
public class PurchaseApplyDetailDto : AuditedEntityDto<Guid>
{

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductUnitName { get; set; }

    public string ProductSpec { get; set; }

    public double Quantity { get; set; }

    public DateTime? ArrivalDate { get; set; }
}