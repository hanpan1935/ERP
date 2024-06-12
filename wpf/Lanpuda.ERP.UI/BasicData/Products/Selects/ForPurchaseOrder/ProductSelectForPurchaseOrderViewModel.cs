using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.ProductCategories;
using Lanpuda.ERP.BasicData.ProductCategories.Dtos;
using Lanpuda.ERP.BasicData.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.BasicData.Products.Selects.ForPurchaseOrder
{
    public class ProductSelectForPurchaseOrderViewModel : PagedViewModelBase<ProductWithPriceDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        private readonly IProductLookupAppService _productLookupAppService;
        private readonly IProductCategoryAppService _productCategoryLookupAppService;
        private readonly IObjectMapper _objectMapper;
       
        public Guid SupplierId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }

        public string SupplierFullName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string SupplierShortName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public ProductWithPriceDto? SelectedRow
        {
            get { return GetValue<ProductWithPriceDto?>(nameof(SelectedRow)); }
            set { SetValue(value, nameof(SelectedRow)); }
        }

        public Action<ProductWithPriceDto>? OnSelectedCallback { get; set; }

        public ObservableCollection<ProductCategoryDto> ProductCategorySource
        {
            get { return GetProperty(() => ProductCategorySource); }
            set { SetProperty(() => ProductCategorySource, value); }
        }



        public ProductSelectForPurchaseOrderViewModel(
            IProductLookupAppService productLookupAppService,
            IProductCategoryAppService productCategoryLookupAppService,
            IObjectMapper objectMapper)
        {
            _productLookupAppService = productLookupAppService;
            _productCategoryLookupAppService = productCategoryLookupAppService;
            _objectMapper = objectMapper;
        }



        #region 搜索
        public string Number
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

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                await GetPagedDatasAsync();
                var categoryList =  await _productCategoryLookupAppService.GetAllAsync();
                ProductCategorySource = new ObservableCollection<ProductCategoryDto>(categoryList);
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

        [Command]
        public void OnSelected()
        {
            if (this.SelectedRow == null)
            {
                return;
            }
            var product = this.PagedDatas.Where(m => m.Id == SelectedRow.Id).First();
            this.OnSelectedCallback?.Invoke(product);
            if (CurrentWindowService != null)
                CurrentWindowService.Close();
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = string.Empty;
            this.ProductCategoryId = null;
            this.Name = string.Empty;
            this.Spec = string.Empty;
            await this.GetPagedDatasAsync();
        }

        [AsyncCommand]
        public async Task PageUpdated(FunctionEventArgs<int> info)
        {
            this.PageIndex = info.Info;
            await GetPagedDatasAsync();
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                ProductWithPurchasePricePagedRequestDto requestDto = new ProductWithPurchasePricePagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.SupplierId = this.SupplierId;
                requestDto.Number = this.Number;
                requestDto.ProductCategoryId = this.ProductCategoryId;
                requestDto.Name = this.Name;
                requestDto.Spec = this.Spec;
                var result = await _productLookupAppService.GetPagedListWithPurchasePriceAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
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
    }
}
