using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;

[Serializable]
public class MaterialReturnApplyDetailDto : EntityDto<Guid>
{
    public Guid MaterialReturnApplyId { get; set; }

    public Guid WorkOrderOutDetailId { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductNumber { get; set; }

    public string ProductSpec { get; set; }

    public string ProductUnitName { get; set; }

    /// <summary>
    /// ��������
    /// </summary>
    public double Quantity { get; set; }

    /// <summary>
    /// ��������
    /// </summary>
    public string Batch { get; set; }

}