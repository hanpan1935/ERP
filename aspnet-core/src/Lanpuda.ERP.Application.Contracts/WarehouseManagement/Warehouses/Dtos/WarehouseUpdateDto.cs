using System;
using System.ComponentModel.DataAnnotations;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;

[Serializable]
public class WarehouseUpdateDto
{
    [Required(ErrorMessage = "±àÂë±ØÌî")]
    public string Number { get; set; }


    [Required(ErrorMessage = "Ãû³Æ±ØÌî")]
    public string Name { get; set; }


    public string Remark { get; set; }
}