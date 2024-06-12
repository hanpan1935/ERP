using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using HandyControl.Controls;
using HandyControl.Data;
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

namespace Lanpuda.ERP.BasicData.Products.Selects.SelectAll
{
    public class ProductSelectAllViewModel : PagedViewModelBase<ProductDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        private readonly IProductLookupAppService _productLookupAppService;
        private readonly IProductCategoryAppService _productCategoryLookupAppService;
        private readonly IObjectMapper _objectMapper;
   

        public ObservableCollection<ProductCategoryDto> CategorySource { get; set; }

        public Action<ProductDto>? OnSelectedCallback { get; set; }


        public ProductSelectAllViewModel(
            IProductLookupAppService productLookupAppService,
            IObjectMapper objectMapper,
            IProductCategoryAppService productCategoryLookupAppService
            )
        {
            _productLookupAppService = productLookupAppService;
            _objectMapper = objectMapper;
            _productCategoryLookupAppService = productCategoryLookupAppService;
            CategorySource = new ObservableCollection<ProductCategoryDto>();
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
                var categoryList = await _productCategoryLookupAppService.GetAllAsync();
                foreach (var item in categoryList)
                {
                    this.CategorySource.Add(item);
                }
                await this.GetPagedDatasAsync();
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

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Name = string.Empty;
            this.Number = string.Empty;
            this.ProductCategoryId = null;
            this.Spec = string.Empty;
            await this.QueryAsync();
        }


        [Command]
        public void OnSelected()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            this.OnSelectedCallback?.Invoke(SelectedModel);
            if (CurrentWindowService != null)
                CurrentWindowService.Close();
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                ProductPagedRequestDto requestDto = new ProductPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = (this.PageIndex - 1) * DataCountPerPage;
                requestDto.Number = this.Number;
                requestDto.ProductCategoryId = this.ProductCategoryId;
                requestDto.Name = this.Name;
                requestDto.Spec = this.Spec;
                var result = await _productLookupAppService.GetPagedListForWorkOrderAsync(requestDto);
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
