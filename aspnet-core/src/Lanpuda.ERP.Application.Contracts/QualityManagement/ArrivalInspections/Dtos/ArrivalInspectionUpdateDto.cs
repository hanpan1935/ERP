using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.QualityManagement.ArrivalInspections.Dtos;

[Serializable]
public class ArrivalInspectionUpdateDto
{
    /// <summary>
    /// ��������
    /// </summary>
    public double BadQuantity { get; set; }

    /// <summary>
    /// �������
    /// </summary>
    public string Description { get; set; }


}