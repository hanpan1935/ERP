using System;

namespace Lanpuda.ERP.QualityManagement.ProcessInspections.Dtos;

[Serializable]
public class ProcessInspectionCreateDto
{
    public double BadQuantity { get; set; }

    public string Description { get; set; }
}