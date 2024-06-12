using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;

[Serializable]
public class WorkOrderOutUpdateDto
{
    public string Remark { get; set; }

    public List<WorkOrderOutDetailUpdateDto> Details { get; set; }

    public WorkOrderOutUpdateDto()
    {
        Details = new List<WorkOrderOutDetailUpdateDto>();
    }
}