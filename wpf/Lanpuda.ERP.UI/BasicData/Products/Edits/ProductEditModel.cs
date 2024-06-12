using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lanpuda.ERP.BasicData.Products.Edits
{
    public class ProductEditModel : ModelBase
    {
        public Guid? Id
        {
            get { return GetValue<Guid?>(nameof(Id)); }
            set { SetValue(value, nameof(Id)); }
        }

        /// <summary>
        /// 产品编码
        /// </summary>
        public string Number
        {
            get { return GetValue<string>(nameof(Number)); }
            set { SetValue(value, nameof(Number)); }
        }

        /// <summary>
        /// 产品分类
        /// </summary>
        public Guid? ProductCategoryId
        {
            get { return GetValue<Guid?>(nameof(ProductCategoryId)); }
            set { SetValue(value, nameof(ProductCategoryId)); }
        }


        /// <summary>
        /// 产品单位
        /// </summary>
        [Required(ErrorMessage = "产品单位必填")]
        [GuidNotEmpty(ErrorMessage = "产品单位必填")]
        public Guid ProductUnitId
        {
            get { return GetValue<Guid>(nameof(ProductUnitId)); }
            set { SetValue(value, nameof(ProductUnitId)); }
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Required(ErrorMessage = "产品名称必填")]
        public string? Name
        {
            get { return GetValue<string>(nameof(Name)); }
            set { SetValue(value, nameof(Name)); }
        }


        /// <summary>
        /// 产品规格
        /// </summary>
        public string? Spec
        {
            get { return GetValue<string>(nameof(Spec)); }
            set { SetValue(value, nameof(Spec)); }
        }






        public ProductSourceType SourceType
        {
            get { return GetValue<ProductSourceType>(nameof(SourceType)); }
            set { SetValue(value, nameof(SourceType)); }
        }


        public double? ProductionBatch
        {
            get { return GetValue<double?>(nameof(ProductionBatch)); }
            set { SetValue(value, nameof(ProductionBatch)); }
        }


        public Guid? DefaultLocationId
        {
            get { return GetValue<Guid?>(nameof(DefaultLocationId)); }
            set { SetValue(value, nameof(DefaultLocationId)); }
        }


        [Range(1, int.MaxValue)]
        public int? LeadTime
        {
            get { return GetValue<int?>(nameof(LeadTime)); }
            set { SetValue(value, nameof(LeadTime)); }
        }




        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark
        {
            get { return GetValue<string>(nameof(Remark)); }
            set { SetValue(value, nameof(Remark)); }
        }

        public bool IsArrivalInspection
        {
            get { return GetProperty(() => IsArrivalInspection); }
            set { SetProperty(() => IsArrivalInspection, value); }
        }

        public bool IsProcessInspection
        {
            get { return GetProperty(() => IsProcessInspection); }
            set { SetProperty(() => IsProcessInspection, value); }
        }

        public bool IsFinalInspection
        {
            get { return GetProperty(() => IsFinalInspection); }
            set { SetProperty(() => IsFinalInspection, value); }
        }


        public Guid? DefaultWorkshopId
        {
            get { return GetProperty(() => DefaultWorkshopId); }
            set { SetProperty(() => DefaultWorkshopId, value); }
        }

        public ObservableCollection<WarehouseDto> WarehouseSource { get; set; }

        public WarehouseDto? SelectedWarehouse
        {
            get { return GetProperty(() => SelectedWarehouse); }
            set { SetProperty(() => SelectedWarehouse, value, OnSelectedWarehouseChanged); }
        }

        public ProductEditModel()
        {
            this.WarehouseSource = new ObservableCollection<WarehouseDto>();
        }

        private void OnSelectedWarehouseChanged()
        {
            if (SelectedWarehouse != null)
            {
                var location = SelectedWarehouse.Locations.FirstOrDefault();
                if (location != null)
                {
                    DefaultLocationId = location.Id;
                }
            }

        }
    }
}
