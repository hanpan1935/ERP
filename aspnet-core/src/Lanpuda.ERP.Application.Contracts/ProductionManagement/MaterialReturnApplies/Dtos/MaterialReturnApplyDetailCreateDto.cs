using System;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;

[Serializable]
public class MaterialReturnApplyDetailCreateDto
{
    public Guid WorkOrderOutDetailId { get; set; }

    /// <summary>
    /// ��������
    /// </summary>
    public double Quantity { get; set; }
}