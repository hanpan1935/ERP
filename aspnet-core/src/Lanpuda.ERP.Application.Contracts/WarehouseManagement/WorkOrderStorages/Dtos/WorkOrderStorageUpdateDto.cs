using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Dtos;

[Serializable]
public class WorkOrderStorageUpdateDto
{
    public string Remark { get; set; }

    public List<WorkOrderStorageDetailUpdateDto> Details { get; set; }

    public WorkOrderStorageUpdateDto()
    {
        this.Details = new List<WorkOrderStorageDetailUpdateDto>();
    }
}