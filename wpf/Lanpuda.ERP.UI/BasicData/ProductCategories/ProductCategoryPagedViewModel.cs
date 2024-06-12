using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.ProductCategories.Dtos;
using Lanpuda.ERP.BasicData.ProductCategories.Edits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.ERP.BasicData.ProductCategories
{
    public class ProductCategoryPagedViewModel : PagedViewModelBase<ProductCategoryDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IProductCategoryAppService _productCategoryAppService;

        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }


        public ProductCategoryPagedViewModel(IServiceProvider serviceProvider, IProductCategoryAppService productCategoryAppService)
        {
            _serviceProvider = serviceProvider;
            _productCategoryAppService = productCategoryAppService;
            this.PageTitle = "产品分类";
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            if (this.WindowService != null)
            {
                ProductCategoryEditViewModel? productCategoryCreateViewModel = (ProductCategoryEditViewModel?)_serviceProvider.GetService(typeof(ProductCategoryEditViewModel));
                if (productCategoryCreateViewModel != null)
                {
                    productCategoryCreateViewModel.Refresh = this.QueryAsync;
                    WindowService.Title = "产品分类-新建";
                    WindowService.Show("ProductCategoryEditView", productCategoryCreateViewModel);
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
                ProductCategoryEditViewModel? productCategoryEditViewModel = (ProductCategoryEditViewModel?)_serviceProvider.GetService(typeof(ProductCategoryEditViewModel));
                if (productCategoryEditViewModel != null)
                {
                    productCategoryEditViewModel.Model.Id = this.SelectedModel.Id;
                    productCategoryEditViewModel.Refresh = this.QueryAsync;
                    WindowService.Title = "产品分类-编辑";
                    WindowService.Show("ProductCategoryEditView", productCategoryEditViewModel);
                }
            }
        }




        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Name = String.Empty;
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
                    await _productCategoryAppService.DeleteAsync(SelectedModel.Id);
                    await this.QueryAsync();
                }
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


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                ProductCategoryPagedRequestDto requestDto = new ProductCategoryPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = (this.PageIndex - 1) * DataCountPerPage;
                requestDto.Name = this.Name;
                var result = await _productCategoryAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
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
