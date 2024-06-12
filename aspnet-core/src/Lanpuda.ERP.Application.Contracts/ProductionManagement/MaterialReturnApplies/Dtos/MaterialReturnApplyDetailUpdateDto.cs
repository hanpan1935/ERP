using System;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;

[Serializable]
public class MaterialReturnApplyDetailUpdateDto
{
    public Guid? Id { get; set; }

    public Guid WorkOrderOutDetailId { get; set; }

    public double Quantity { get; set; }
}