using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.ProductCategories;
using Lanpuda.ERP.BasicData.ProductCategories.Dtos;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products.Edits;
using Lanpuda.ERP.BasicData.ProductUnits;
using Lanpuda.ERP.BasicData.ProductUnits.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;
using System.Windows.Controls;

namespace Lanpuda.ERP.BasicData.Products
{
    public class ProductPagedViewModel : PagedViewModelBase<ProductDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IProductAppService _productAppService;
        private readonly IProductCategoryAppService _productCategoryAppService;
        private readonly IProductUnitAppService _productUnitLookupAppService;
        private readonly IObjectMapper _objectMapper;


        #region 搜索
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


        public ProductPagedViewModel(
            IServiceProvider serviceProvider,
            IProductAppService productAppService,
            IProductUnitAppService productUnitLookupAppService,
            IProductCategoryAppService productCategoryAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _productAppService = productAppService;
            _productUnitLookupAppService = productUnitLookupAppService;
            _productCategoryAppService = productCategoryAppService;
            _objectMapper = objectMapper;
            ProductCategorySourceList = new ObservableCollection<ProductCategoryDto>();
            ProductUnitSourceList = new ObservableCollection<ProductUnitDto>();
            this.PageTitle = "产品信息";
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


        [Command]
        public void Create()
        {
            if (this.WindowService != null)
            {
                ProductEditViewModel? productCreateViewModel = (ProductEditViewModel?)_serviceProvider.GetService(typeof(ProductEditViewModel));
                if (productCreateViewModel != null)
                {
                    productCreateViewModel.Refresh = this.QueryAsync;
                    WindowService.Title = "产品信息-新建";
                    WindowService.Show("ProductEditView", productCreateViewModel);
                }
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            if (this.WindowService != null)
            {
                ProductEditViewModel? productEditViewModel = (ProductEditViewModel?)_serviceProvider.GetService(typeof(ProductEditViewModel));
                if (productEditViewModel != null)
                {
                    productEditViewModel.Model.Id = this.SelectedModel.Id;
                    productEditViewModel.Refresh = this.QueryAsync;
                    WindowService.Title = "产品信息-编辑";
                    WindowService.Show("ProductEditView", productEditViewModel);
                }
            }
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

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _productAppService.DeleteAsync(this.SelectedModel.Id);
                    await this.QueryAsync();
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

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                ProductPagedRequestDto requestDto = new ProductPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.ProductCategoryId = this.ProductCategoryId;
                requestDto.Name = this.Name;
                requestDto.Spec = this.Spec;
                var result = await _productAppService.GetPagedListAsync(requestDto);
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
