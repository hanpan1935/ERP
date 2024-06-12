using System;

namespace Lanpuda.ERP.QualityManagement.FinalInspections.Dtos;

[Serializable]
public class FinalInspectionCreateDto
{
    public string Number { get; set; }

    public double BadQuantity { get; set; }

    public string Description { get; set; }
}