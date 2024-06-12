using System;
using System.ComponentModel.DataAnnotations;

namespace Lanpuda.ERP.BasicData.Products.Dtos;

[Serializable]
public class ProductUpdateDto
{
    public string Number { get; set; }

    public Guid? ProductCategoryId { get; set; }

    [Required]
    public Guid ProductUnitId { get; set; }

    [Required]
    public string Name { get; set; }


    public string Spec { get; set; }


    public ProductSourceType SourceType { get; set; }

    public double? ProductionBatch { get; set; }

    public Guid? DefaultLocationId { get; set; }

    public int? LeadTime { get; set; }


    #region ÷ ºÏ–≈œ¢
    public bool IsArrivalInspection { get; set; }

    public bool IsProcessInspection { get; set; }

    public bool IsFinalInspection { get; set; }

    #endregion

    public string Remark { get; set; }

    public Guid? DefaultWorkshopId { get; set; }
}