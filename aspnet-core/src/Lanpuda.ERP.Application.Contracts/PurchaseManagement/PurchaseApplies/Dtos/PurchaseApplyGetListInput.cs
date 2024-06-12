using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;

[Serializable]
public class PurchaseApplyGetListInput : PagedAndSortedResultRequestDto
{
    public string? Number { get; set; }

    public Guid? MrpId { get; set; }

    public string? MrpNumber { get; set; }

    public PurchaseApplyType? ApplyType { get; set; }

    public bool? IsConfirmed { get; set; }

    public bool? IsAccept { get; set; }

}