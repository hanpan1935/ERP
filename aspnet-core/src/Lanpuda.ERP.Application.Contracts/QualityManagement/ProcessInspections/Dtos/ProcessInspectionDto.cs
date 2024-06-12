using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.QualityManagement.ProcessInspections.Dtos;

[Serializable]
public class ProcessInspectionDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public string WorkOrderNumber { get; set; }

    public string ProductName { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }

    public double WorkOrderQuantity { get; set; }

    public double BadQuantity { get; set; }

    public string Description { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }

    public bool IsConfirmed { get; set; }
    public DateTime? ConfirmedTime { get; set; }
    public Guid? ConfirmeUserId { get; set; }
    public string ConfirmeUserSurname { get; set; }
    public string ConfirmeUserName { get; set; }
}