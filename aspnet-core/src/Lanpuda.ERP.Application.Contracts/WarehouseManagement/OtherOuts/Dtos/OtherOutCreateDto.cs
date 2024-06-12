using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts.Dtos;

[Serializable]
public class OtherOutCreateDto
{
    [Required]
    public string Description { get; set; }

    public List<OtherOutDetailCreateDto> Details { get; set; }

    public OtherOutCreateDto()
    {
        Details= new List<OtherOutDetailCreateDto>();
    }
}