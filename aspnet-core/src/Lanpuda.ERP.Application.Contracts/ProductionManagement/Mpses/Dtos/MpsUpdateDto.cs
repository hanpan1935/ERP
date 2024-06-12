using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Dtos;

[Serializable]
public class MpsUpdateDto
{

    public MpsType MpsType { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime CompleteDate { get; set; }

    public Guid ProductId { get; set; }

    public double Quantity { get; set; }

    public string Remark { get; set; }

    public List<MpsDetailUpdateDto> Details { get; set; }

    public MpsUpdateDto()
    {
        Details = new List<MpsDetailUpdateDto>();
    }

}