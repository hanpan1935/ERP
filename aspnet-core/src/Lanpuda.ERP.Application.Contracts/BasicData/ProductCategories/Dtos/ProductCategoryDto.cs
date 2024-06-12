using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.BasicData.ProductCategories.Dtos;

[Serializable]
public class ProductCategoryDto : AuditedEntityDto<Guid>
{
    [Required]
    [MaxLength(128)]
    public string Name { get; set; }


    [MaxLength(128)]
    public string Number { get; set; }


    [MaxLength(256)]
    public string Remark { get; set; }

    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}