using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.QualityManagement.ArrivalInspections.Dtos;

[Serializable]
public class ArrivalInspectionUpdateDto
{
    /// <summary>
    /// 不良数量
    /// </summary>
    public double BadQuantity { get; set; }

    /// <summary>
    /// 情况描述
    /// </summary>
    public string Description { get; set; }


}