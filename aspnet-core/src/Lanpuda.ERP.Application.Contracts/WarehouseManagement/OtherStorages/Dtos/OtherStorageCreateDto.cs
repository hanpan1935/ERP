using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lanpuda.ERP.WarehouseManagement.OtherStorages.Dtos;

[Serializable]
public class OtherStorageCreateDto
{
    [Required]
    public string Description { get; set; }


    public List<OtherStorageDetailCreateDto> Details { get; set; }


    public OtherStorageCreateDto()
    {
        Details = new List<OtherStorageDetailCreateDto>();
    }
}