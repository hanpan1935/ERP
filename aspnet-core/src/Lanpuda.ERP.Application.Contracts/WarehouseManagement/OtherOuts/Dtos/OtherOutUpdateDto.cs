using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts.Dtos;

[Serializable]
public class OtherOutUpdateDto
{
    [Required]
    public string Description { get; set; }

    public List<OtherOutDetailUpdateDto> Details { get; set; }

    public OtherOutUpdateDto()
    {
        Details = new List<OtherOutDetailUpdateDto>();
    }
}