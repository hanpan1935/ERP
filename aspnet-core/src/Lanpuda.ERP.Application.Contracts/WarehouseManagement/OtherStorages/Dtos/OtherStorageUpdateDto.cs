using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages.Dtos;

[Serializable]
public class OtherStorageUpdateDto
{
    [Required]
    public string Description { get; set; }

    public List<OtherStorageDetailUpdateDto> Details { get; set; }

    public OtherStorageUpdateDto()
    {
        Details = new List<OtherStorageDetailUpdateDto>();
    }
}