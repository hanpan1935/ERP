using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Utils;
using Lanpuda.ERP.BasicData.ProductCategories;
using Lanpuda.ERP.BasicData.ProductCategories.Dtos;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.ProductUnits;
using Lanpuda.ERP.BasicData.ProductUnits.Dtos;
using Lanpuda.ERP.ProductionManagement.Workshops;
using Lanpuda.ERP.ProductionManagement.Workshops.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lanpuda.ERP.BasicData.Products.Edits
{
    public class ProductEditViewModel : EditViewModelBase<ProductEditModel>
    {
        private readonly IProductAppService _productAppService;
        private readonly IProductCategoryAppService _productCategoryAppService;
        private readonly IProductUnitAppService _productUnitAppService;
        private readonly IWarehouseAppService _warehouseLookupAppService;
        private readonly IWorkshopAppService _workshopAppService;

        public Func<Task>? Refresh { get; set; }


        public ObservableCollection<ProductCategoryDto> ProductCategoryList { get; set; }
        public ObservableCollection<ProductUnitDto> ProductUnitList { get; set; }
        public ObservableCollection<WorkshopDto> WorkshopSource { get; set; }
        public Dictionary<string, ProductSourceType> ProductSourceTypeList { get; set; }
        public Dictionary<string, bool> IsArrivalInspectionSource { get; set; }
        public Dictionary<string, bool> IsProcessInspectionSource { get; set; }
        public Dictionary<string, bool> IsFinalInspectionSource { get; set; }



        public ProductEditViewModel(
                IProductAppService productAppService,
                IProductUnitAppService productUnitAppService,
                IProductCategoryAppService productCategoryAppService,
                IWarehouseAppService warehouseLookupAppService,
                IWorkshopAppService workshopAppService
                )
        {
            _productAppService = productAppService;
            _productUnitAppService = productUnitAppService;
            _productCategoryAppService = productCategoryAppService;
            _warehouseLookupAppService = warehouseLookupAppService;
            _workshopAppService = workshopAppService;
            ProductCategoryList = new ObservableCollection<ProductCategoryDto>();
            ProductUnitList = new ObservableCollection<ProductUnitDto>();
            ProductSourceTypeList = EnumUtils.EnumToDictionary<ProductSourceType>();
            WorkshopSource = new ObservableCollection<WorkshopDto>();
            IsArrivalInspectionSource = ItemsSoureUtils.GetBoolDictionary();
            IsProcessInspectionSource = ItemsSoureUtils.GetBoolDictionary();
            IsFinalInspectionSource = ItemsSoureUtils.GetBoolDictionary();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var categoryList = await _productCategoryAppService.GetAllAsync();
                var unitList = await _productUnitAppService.GetAllAsync();
                var warehouseList = await _warehouseLookupAppService.GetAllAsync();
                var workshopList = await _workshopAppService.GetAllAsync();
                this.ProductCategoryList.Clear();
                this.ProductUnitList.Clear();
                this.Model.WarehouseSource.Clear();
                foreach (var item in categoryList)
                {
                    ProductCategoryList.Add(item);
                }
                foreach (var item in unitList)
                {
                    ProductUnitList.Add(item);
                }

                foreach (var item in warehouseList)
                {
                    Model.WarehouseSource.Add(item);
                }

                foreach (var item in workshopList)
                {
                    this.WorkshopSource.Add(item);
                }

                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _productAppService.GetAsync(id);
                    this.Model.Name = result.Name;
                    Model.Number = result.Number;
                    Model.ProductCategoryId = result.ProductCategoryId;
                    Model.ProductUnitId = result.ProductUnitId;
                    Model.Name = result.Name;
                    Model.Spec = result.Spec;
                    Model.SourceType = result.SourceType;
                    Model.ProductionBatch = result.ProductionBatch;

                    Model.SelectedWarehouse = Model.WarehouseSource.Where(m => m.Id == result.DefaultWarehouseId).FirstOrDefault();

                    Model.DefaultLocationId = result.DefaultLocationId;
                    Model.LeadTime = result.LeadTime;
                    Model.Remark = result.Remark;
                    Model.IsArrivalInspection = result.IsArrivalInspection;
                    Model.IsProcessInspection = result.IsProcessInspection;
                    Model.IsFinalInspection = result.IsFinalInspection;
                    Model.DefaultWorkshopId = result.DefaultWorkshopId;
                }

            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally
            {
                this.IsLoading = false;
            }
        }



        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null)
            {
                await CreateAsync();
            }
            else
            {
                await UpdateAsync();
            }

            if (Refresh != null)
            {
                await Refresh.Invoke();
                this.Close();
            }
        }


        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }


        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                ProductCreateDto dto = new ProductCreateDto();
                dto.Number = Model.Number;
                dto.ProductCategoryId = Model.ProductCategoryId;
                dto.ProductUnitId = Model.ProductUnitId;
                dto.Name = Model.Name;
                dto.Spec = Model.Spec;
                dto.SourceType = Model.SourceType;
                dto.ProductionBatch = Model.ProductionBatch;
                dto.DefaultLocationId= Model.DefaultLocationId;
                dto.LeadTime = Model.LeadTime;
                dto.Remark = Model.Remark;
                dto.IsArrivalInspection= Model.IsArrivalInspection;
                dto.IsProcessInspection= Model.IsProcessInspection;
                dto.IsFinalInspection = Model.IsFinalInspection;
                dto.DefaultWorkshopId = Model.DefaultWorkshopId;
                await _productAppService.CreateAsync(dto);

            }
            catch (Exception e)
            {
                HandleException(e);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                ProductUpdateDto dto = new ProductUpdateDto();
                dto.Number = Model.Number;
                dto.ProductCategoryId = Model.ProductCategoryId;
                dto.ProductUnitId = Model.ProductUnitId;
                dto.Name = Model.Name;
                dto.Spec = Model.Spec;
                dto.SourceType = Model.SourceType;
                dto.ProductionBatch = Model.ProductionBatch;
                dto.DefaultLocationId = Model.DefaultLocationId;
                dto.LeadTime = Model.LeadTime;
                dto.Remark = Model.Remark;
                dto.IsArrivalInspection = Model.IsArrivalInspection;
                dto.IsProcessInspection = Model.IsProcessInspection;
                dto.IsFinalInspection = Model.IsFinalInspection;
                dto.DefaultWorkshopId = Model.DefaultWorkshopId;
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _productAppService.UpdateAsync((Guid)this.Model.Id, dto);

            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally
            {
                this.IsLoading = false;
            }
        }
    }
}
