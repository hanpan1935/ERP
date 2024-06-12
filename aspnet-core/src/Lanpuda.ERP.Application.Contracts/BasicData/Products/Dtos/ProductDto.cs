using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.BasicData.Products.Dtos;

[Serializable]
public class ProductDto : AuditedEntityDto<Guid>
{
    [Required]
    public string Number { get; set; }

    public Guid? ProductCategoryId { get; set; }

    public string ProductCategoryName { get; set; }

    public Guid ProductUnitId { get; set; }

    public string ProductUnitName { get; set; }

    public string Name { get; set; }

    public string Spec { get; set; }

    public ProductSourceType SourceType { get; set; }


    /// <summary>
    /// 生产批量
    /// </summary>
    public double? ProductionBatch { get; set; }


    public int? LeadTime { get; set; }

    public Guid? DefaultLocationId { get; set; }
    public string DefaultLocationName { get; set; }

    public Guid? DefaultWarehouseId { get; set; }
    public string DefaultWarehouseName { get; set; }


    #region 质检信息
    public bool IsArrivalInspection { get; set; }

    public bool IsProcessInspection { get; set; }

    public bool IsFinalInspection { get; set; }

    #endregion


    public Guid? DefaultWorkshopId { get; set; }
    public string DefaultWorkshopName { get; set; }

    public string Remark { get; set; }


    public string CreatorSurname { get; set; }

    public string CreatorName { get; set; }
}