using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Dtos;

[Serializable]
public class WorkOrderStorageApplyUpdateDto
{
    public Guid WorkOrderId { get; set; }

    public double Quantity { get; set; }

    public string Remark { get; set; }


    public WorkOrderStorageApplyUpdateDto()
    {
    }
}