using System;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;

[Serializable]
public class MaterialReturnApplyDetailCreateDto
{
    public Guid WorkOrderOutDetailId { get; set; }

    /// <summary>
    /// ÍËÁÏÊıÁ¿
    /// </summary>
    public double Quantity { get; set; }
}