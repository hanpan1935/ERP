using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;

[Serializable]
public class PurchaseReturnApplyDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public Guid SupplierId { get; set; }

    public string SupplierShortName { get; set; }

    public string SupplierFullName { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public PurchaseReturnReason ReturnReason { get; set; }

    public string Description { get; set; }

    public string Remark { get; set; }

    public bool IsConfirmed { get; set; }

    public DateTime? ConfirmedTime { get; set; }

    public string ConfirmeUserName { get; set; }
    public string ConfirmeUserSurname { get; set; }


    public List<PurchaseReturnApplyDetailDto> Details { get; set; }


    public PurchaseReturnApplyDto()
    {
        this.Details = new List<PurchaseReturnApplyDetailDto>();
    }
}