using System;
using System.Collections.Generic;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Dtos;

[Serializable]
public class MpsCreateDto
{

    public MpsType MpsType { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime CompleteDate { get; set; }

    public Guid ProductId { get; set; }

    public double Quantity { get; set; }

    public string Remark { get; set; }

    public List<MpsDetailCreateDto> Details { get; set; }


    public MpsCreateDto()
    {
        Details = new List<MpsDetailCreateDto>();
    }

}