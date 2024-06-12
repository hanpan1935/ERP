using System;

namespace Lanpuda.ERP.QualityManagement.ProcessInspections.Dtos;

[Serializable]
public class ProcessInspectionUpdateDto
{

    public double BadQuantity { get; set; }

    public string Description { get; set; }
}