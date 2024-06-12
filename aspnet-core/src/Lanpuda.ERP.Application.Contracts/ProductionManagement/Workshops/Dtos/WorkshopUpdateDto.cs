using System;

namespace Lanpuda.ERP.ProductionManagement.Workshops.Dtos;

[Serializable]
public class WorkshopUpdateDto
{
    public string Number { get; set; }

    public string Name { get; set; }

    public string Remark { get; set; }
}