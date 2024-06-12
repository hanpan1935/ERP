using System;
using System.ComponentModel.DataAnnotations;

namespace Lanpuda.ERP.BasicData.ProductCategories.Dtos;

[Serializable]
public class ProductCategoryUpdateDto
{
    [Required(ErrorMessage = "Ãû³Æ±ØÌî")]
    [MaxLength(128)]
    public string Name { get; set; }

    [MaxLength(128)]
    public string Number { get; set; }


    [MaxLength(256)]
    public string Remark { get; set; }
}