using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.ProductUnits.Dtos;
using Lanpuda.ERP.BasicData.ProductUnits.Edits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.DependencyInjection;

namespace Lanpuda.ERP.BasicData.ProductUnits
{
    public class ProductUnitPagedViewModel : PagedViewModelBase<ProductUnitDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        private readonly IServiceProvider _serviceProvider;
        private readonly IProductUnitAppService _productUnitAppService;


        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }


        public ProductUnitPagedViewModel(IServiceProvider serviceProvider, IProductUnitAppService productUnitAppService)
        {
            _serviceProvider = serviceProvider;
            _productUnitAppService = productUnitAppService;
            this.PageTitle = "产品单位";
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
            //await Task.Delay(1000);
        }


        [Command]
        public void Create()
        {
            if (this.WindowService != null)
            {
                ProductUnitEditViewModel? productUnitCreateViewModel = (ProductUnitEditViewModel?)_serviceProvider.GetService(typeof(ProductUnitEditViewModel));
                if (productUnitCreateViewModel != null)
                {
                    productUnitCreateViewModel.OnCloseCallbackAsync = this.QueryAsync;
                    WindowService.Title = "产品单位-新建";
                    WindowService.Show("ProductUnitEditView", productUnitCreateViewModel);
                }
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null) { return; }
            if (this.WindowService != null)
            {
                ProductUnitEditViewModel? productUnitEditViewModel = (ProductUnitEditViewModel?)_serviceProvider.GetService(typeof(ProductUnitEditViewModel));
                if (productUnitEditViewModel != null)
                {
                    productUnitEditViewModel.Model.Id = this.SelectedModel.Id;
                    productUnitEditViewModel.OnCloseCallbackAsync = this.QueryAsync;
                    WindowService.Title = "产品单位-编辑";
                    WindowService.Show("ProductUnitEditView", productUnitEditViewModel);
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
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _productUnitAppService.DeleteAsync(this.SelectedModel.Id);
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
                ProductUnitPagedRequestDto requestDto = new ProductUnitPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = (this.PageIndex - 1) * DataCountPerPage;
                requestDto.Name = this.Name;
                var result = await _productUnitAppService.GetPagedListAsync(requestDto);
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
