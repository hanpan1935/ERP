using System;
using System.ComponentModel.DataAnnotations;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;

[Serializable]
public class WarehouseUpdateDto
{
    [Required(ErrorMessage = "�������")]
    public string Number { get; set; }


    [Required(ErrorMessage = "���Ʊ���")]
    public string Name { get; set; }


    public string Remark { get; set; }
}