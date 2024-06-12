using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.ProductCategories.Dtos;
using Lanpuda.ERP.BasicData.ProductCategories;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.ProductUnits.Dtos;
using Lanpuda.ERP.BasicData.ProductUnits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.BasicData.Products.Selects.MultipleSelectWithPurchasePrice
{
    public class MultipleSelectWithPurchasePriceViewModel : PagedViewModelBase<ProductWithPriceDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        private readonly IServiceProvider _serviceProvider;
        private readonly IProductLookupAppService _productLookupAppService;
        private readonly IProductCategoryAppService _productCategoryAppService;
        private readonly IProductUnitAppService _productUnitLookupAppService;
        private readonly IObjectMapper _objectMapper;

        public Action<ICollection<ProductWithPriceDto>>? SaveCallback;

        /// <summary>
        /// 右侧表格数据
        /// </summary>
        public ObservableCollection<ProductWithPriceDto> SelectedProductList { get; set; }
        /// <summary>
        /// 右侧表格选中的product
        /// </summary>
        public ProductWithPriceDto? SelectedProduct
        {
            get { return GetProperty(() => SelectedProduct); }
            set { SetProperty(() => SelectedProduct, value); }
        }

        #region 搜索
        public Guid? CustomerId { get; set; }

        public Guid? SupplierId { get; set; }

        public string? Number
        {
            get { return GetValue<string>(nameof(Number)); }
            set { SetValue(value, nameof(Number)); }
        }

        public Guid? ProductCategoryId
        {
            get { return GetValue<Guid?>(nameof(ProductCategoryId)); }
            set { SetValue(value, nameof(ProductCategoryId)); }
        }

        public string Name
        {
            get { return GetValue<string>(nameof(Name)); }
            set { SetValue(value, nameof(Name)); }
        }

        public string Spec
        {
            get { return GetValue<string>(nameof(Spec)); }
            set { SetValue(value, nameof(Spec)); }
        }
        #endregion

        #region 搜索数据源
        public ObservableCollection<ProductCategoryDto> ProductCategorySourceList { get; set; }
        public ObservableCollection<ProductUnitDto> ProductUnitSourceList { get; set; }
        #endregion


        public MultipleSelectWithPurchasePriceViewModel(
            IServiceProvider serviceProvider,
            IProductLookupAppService productLookupAppService,
            IProductUnitAppService productUnitLookupAppService,
            IProductCategoryAppService productCategoryAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _productLookupAppService = productLookupAppService;
            _productUnitLookupAppService = productUnitLookupAppService;
            _productCategoryAppService = productCategoryAppService;
            _objectMapper = objectMapper;
            ProductCategorySourceList = new ObservableCollection<ProductCategoryDto>();
            ProductUnitSourceList = new ObservableCollection<ProductUnitDto>();
            SelectedProductList = new ObservableCollection<ProductWithPriceDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            var categoryList = await _productCategoryAppService.GetAllAsync();
            var unitList = await _productUnitLookupAppService.GetAllAsync();
            this.ProductCategorySourceList.Clear();
            this.ProductUnitSourceList.Clear();
            foreach (var item in categoryList)
            {
                ProductCategorySourceList.Add(item);
            }

            foreach (var item in unitList)
            {
                ProductUnitSourceList.Add(item);
            }

            await this.QueryAsync();
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = string.Empty;
            this.ProductCategoryId = null;
            this.Name = string.Empty;
            this.Spec = string.Empty;
            await QueryAsync();
        }


        [Command]
        public void Select()
        {
            if (this.SelectedModel != null)
            {
                //判断是否已经添加
                var product = SelectedProductList.Where(m => m.Id == SelectedModel.Id).FirstOrDefault();
                if (product != null)
                {
                    this.SelectedProduct = product;
                    Growl.Info("已经添加了");
                }
                else
                {
                    this.SelectedProductList.Add(SelectedModel);
                }
            }
        }

        [Command]
        public void Delete()
        {
            if (this.SelectedProduct != null)
            {
                this.SelectedProductList.Remove(SelectedProduct);
            }
        }

        [Command]
        public void Close()
        {
            if (CurrentWindowService != null)
                CurrentWindowService.Close();
        }


        [Command]
        public void Save()
        {
            if (this.SaveCallback != null)
            {
                SaveCallback(this.SelectedProductList);
                this.Close();
            }
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                ProductWithPurchasePricePagedRequestDto requestDto = new ProductWithPurchasePricePagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.ProductCategoryId = this.ProductCategoryId;
                requestDto.Name = this.Name;
                requestDto.Spec = this.Spec;
                if (this.SupplierId != null)
                {
                    requestDto.SupplierId = (Guid)this.SupplierId;
                }
                var result = await _productLookupAppService.GetPagedListWithPurchasePriceAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.Clear();
                this.PagedDatas.CanNotify = false;
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                this.PagedDatas.CanNotify = true;
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
