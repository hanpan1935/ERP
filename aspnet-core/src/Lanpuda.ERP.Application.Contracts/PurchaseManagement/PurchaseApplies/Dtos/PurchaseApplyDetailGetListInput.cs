using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;

[Serializable]
public class PurchaseApplyDetailGetListInput : PagedAndSortedResultRequestDto
{
    public Guid? PurchaseApplyId { get; set; }

    public Guid? ProductId { get; set; }

    public double? Quantity { get; set; }

    public DateTime? ArrivalDate { get; set; }


}