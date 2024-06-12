using System;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;

[Serializable]
public class WorkOrderOutDetailUpdateDto
{
    public Guid? Id { get; set; }


    public Guid LocationId { get; set; }

    public string Batch { get; set; }

    public double Quantity { get; set; }


}