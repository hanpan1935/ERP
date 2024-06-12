using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Dtos;

[Serializable]
public class WorkOrderReturnUpdateDto
{
    public string Remark { get; set; }

    public List<WorkOrderReturnDetailUpdateDto> Details { get; set; }

    public WorkOrderReturnUpdateDto()
    {
        Details = new List<WorkOrderReturnDetailUpdateDto>();
    }
}