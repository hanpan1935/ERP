using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.QualityManagement.FinalInspections.Dtos;

[Serializable]
public class FinalInspectionDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public string MpsNumber { get; set; }

    public string ProductName { get; set; }

    public string ProductSpec { get; set; }


    public string ProductUnitName { get; set; }

    public double MpsQuantity { get; set; }

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