using System;
using System.ComponentModel.DataAnnotations;

namespace Lanpuda.ERP.BasicData.ProductUnits.Dtos;

[Serializable]
public class ProductUnitCreateDto
{
    [Required]
    [MaxLength(128)]
    public string Name { get; set; }


    [MaxLength(128)]
    public string Number { get; set; }


    [MaxLength(256)]
    public string Remark { get; set; }
}