using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Dtos;

[Serializable]
public class WorkOrderStorageApplyDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public Guid WorkOrderId { get; set; }
    public string WorkOrderNumber { get; set; }

    public string ProductName { get; set; }

    public string MpsNumber { get; set; }

    public string WorkOrderStorageNumber { get; set; }
    

    public double Quantity { get; set; }

    public string Remark { get; set; }

    public bool IsConfirmed { get; set; }

    public string ConfirmedUserSurname { get; set; }

    public string ConfirmedUserName { get; set; }

    public DateTime? ConfirmedTime { get; set; }



    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}