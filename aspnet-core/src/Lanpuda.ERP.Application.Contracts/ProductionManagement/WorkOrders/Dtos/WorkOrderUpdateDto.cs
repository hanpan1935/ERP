using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;

[Serializable]
public class WorkOrderUpdateDto
{
    public Guid MpsId { get; set; }

    public Guid? WorkshopId { get; set; }

    public Guid ProductId { get; set; }

    public double Quantity { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    [MaxLength(256)]
    public string Remark { get; set; }


    public WorkOrderUpdateDto()
    {
    }
}