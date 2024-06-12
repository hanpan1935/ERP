using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;

[Serializable]
public class MaterialReturnApplyDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public string Remark { get; set; }
   

    public bool IsConfirmed { get; set; }
    public DateTime? ConfirmedTime { get; set; }
    public Guid? ConfirmedUserId { get; set; }
    public string ConfirmedUserName { get; set; }
    public string ConfirmedUserSurname { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public List<MaterialReturnApplyDetailDto> Details { get; set; }
}