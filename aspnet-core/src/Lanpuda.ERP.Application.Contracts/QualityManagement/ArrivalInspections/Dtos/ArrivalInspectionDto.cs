using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.QualityManagement.ArrivalInspections.Dtos;

[Serializable]
public class ArrivalInspectionDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }


    public Guid ArrivalNoticeDetailId { get; set; }

    public string ArrivalNoticeNumber { get; set; }

    public string PurchaseStorageNumber { get; set; }


    public double ArrivalNoticeQuantity { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductUnitName { get; set; }

    public string ProductSpec { get; set; }

    /// <summary>
    /// 不良数量
    /// </summary>
    public double BadQuantity { get; set; }

    /// <summary>
    /// 情况描述
    /// </summary>
    public string Description { get; set; }



    public bool IsConfirmed { get; set; }

    public DateTime? ConfirmedTime { get; set; }

    public Guid? ConfirmeUserId { get; set; }

    public string ConfirmeUserSurname { get; set; }

    public string ConfirmeUserName { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}