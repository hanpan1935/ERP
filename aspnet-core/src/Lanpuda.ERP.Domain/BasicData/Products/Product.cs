using Lanpuda.ERP.BasicData.ProductCategories;
using Lanpuda.ERP.BasicData.ProductUnits;
using Lanpuda.ERP.ProductionManagement.Boms;
using Lanpuda.ERP.ProductionManagement.Workshops;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.ERP.BasicData.Products
{
    /// <summary>
    /// 产品
    /// </summary>
    public class Product : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        [MaxLength(128)]
        public string Number { get; set; }

        /// <summary>
        /// 产品分类
        /// </summary>
        public Guid? ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        /// <summary>
        /// 产品单位
        /// </summary>
        public Guid ProductUnitId { get; set; }
        public ProductUnit ProductUnit { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        [MaxLength(128)]
        public string Spec { get; set; }

        /// <summary>
        /// 产品来源
        /// </summary>
        public ProductSourceType SourceType { get; set; }


        /// <summary>
        /// 生产批量
        /// </summary>
        public double? ProductionBatch { get; set; }


        public Guid? DefaultLocationId { get; set; }
        public Location DefaultLocation { get; set; }




        /// <summary>
        /// 提前期
        /// </summary>
        /// 
        [Range(1,int.MaxValue)]
        public int? LeadTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }


        #region 质检信息
        public bool IsArrivalInspection { get; set; }

        public bool IsProcessInspection { get; set; }

        public bool IsFinalInspection { get; set; }

        #endregion

       
        public Guid? DefaultWorkshopId { get; set; }
        public Workshop DefaultWorkshop { get; set; }

        /// <summary>
        /// 1对1 生产bom
        /// </summary>
        public Bom Bom { get; set; }

        /// <summary>
        /// 1对1 安全库存
        /// </summary>
        public SafetyInventory SafetyInventory { get; set; }

        public IdentityUser Creator { get; set; }

        protected Product()
        {
        }


        public Product(Guid id) : base(id)
        {
        }
    }
}
