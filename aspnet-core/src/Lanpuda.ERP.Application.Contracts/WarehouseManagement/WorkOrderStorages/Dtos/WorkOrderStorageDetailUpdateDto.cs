using System;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Dtos;

[Serializable]
public class WorkOrderStorageDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid LocationId { get; set; }

    public double Quantity { get; set; }
}