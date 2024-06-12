using System;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Dtos;

[Serializable]
public class MpsDetailCreateDto
{
    public Guid MpsId { get; set; }

    public DateTime ProductionDate { get; set; }

    public double Quantity { get; set; }

    public string Remark { get; set; }
}