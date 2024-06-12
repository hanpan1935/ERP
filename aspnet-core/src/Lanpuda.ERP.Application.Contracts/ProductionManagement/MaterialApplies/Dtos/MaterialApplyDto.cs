using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;

[Serializable]
public class MaterialApplyDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public Guid WorkOrderId { get; set; }

    public string WorkOrderNumber { get; set; }

    public string Remark { get; set; }


    public string MpsNumber { get; set; }


    public string ProductName { get; set; }



    public List<MaterialApplyDetailDto> Details { get; set; }


    /// <summary>
    /// »∑»œ»À
    /// </summary>
    public bool IsConfirmed { get; set; }
    public DateTime? ConfirmedTime { get; set; }
    public Guid? ConfirmedUserId { get; set; }
    public string ConfirmedUserSurname { get; set; }
    public string ConfirmedUserName { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}