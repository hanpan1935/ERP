using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;

[Serializable]
public class PurchaseApplyDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public PurchaseApplyType ApplyType { get; set; }

    public Guid? MpsId { get; set; }
    public string MpsNumber { get; set; }


    public string CreatorSurname { get; set; }
    public string CreatorName { get; set; }

    public string Remark { get; set; }

    public bool IsConfirmed { get; set; }

    public DateTime? ConfirmedTime { get; set; }

    public Guid? ConfirmeUserId { get; set; }

    public string ConfirmeUserName { get; set; }
    public string ConfirmeUserSurname { get; set; }

    public bool IsAccept { get; set; }

    public DateTime? AcceptTime { get; set; }

    public Guid? AcceptUserId { get; set; }

    public string AcceptUserName { get; set; }
    public string AcceptUserSurname { get; set; }

    public List<PurchaseApplyDetailDto> Details { get; set; }
}