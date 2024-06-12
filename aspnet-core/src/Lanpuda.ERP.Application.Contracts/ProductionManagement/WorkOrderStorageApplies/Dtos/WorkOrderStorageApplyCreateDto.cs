using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Dtos;

[Serializable]
public class WorkOrderStorageApplyCreateDto
{
    public Guid WorkOrderId { get; set; }

    public double Quantity { get; set; }

    public string Remark { get; set; }


    public WorkOrderStorageApplyCreateDto()
    {
    }
}