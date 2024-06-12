using System;

namespace Lanpuda.ERP.QualityManagement.FinalInspections.Dtos;

[Serializable]
public class FinalInspectionUpdateDto
{
    public double BadQuantity { get; set; }

    public string Description { get; set; }
}