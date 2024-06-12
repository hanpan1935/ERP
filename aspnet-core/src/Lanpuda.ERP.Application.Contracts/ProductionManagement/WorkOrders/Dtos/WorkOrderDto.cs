using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;

[Serializable]
public class WorkOrderDto : AuditedEntityDto<Guid>
{
    public string Number { get; set; }

    public Guid? WorkshopId { get; set; }

    public string WorkshopName { get;set; }

    public Guid ProductId { get; set; }

    public string ProductNumber { get; set; }

    public string ProductName { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }

    public Guid MpsId { get; set; }

    public string MpsNumber { get; set; }


    public double Quantity { get; set; }


    public DateTime StartDate { get; set; }


    public DateTime? CompletionDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(256)]
    public string Remark { get; set; }


    public bool IsConfirmed { get; set; }
    public DateTime? ConfirmedTime { get; set; }
    public Guid? ConfirmedUserId { get; set; }
    public string ConfirmedUserName { get; set; }
    public string ConfirmedUserSurname { get; set; }


    public List<WorkOrderMaterialDto> StandardMaterialDetails { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }


    public WorkOrderDto()
    {
        StandardMaterialDetails = new List<WorkOrderMaterialDto>();
    }

}