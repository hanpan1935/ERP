using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products.Selects.SelectAll;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories.Edits
{
    public class SafetyInventoryEditViewModel : EditViewModelBase<SafetyInventoryEditModel>
    {
        private readonly ISafetyInventoryAppService _safetyInventoryAppService;
        private readonly IServiceProvider _serviceProvider;
        public Func<Task>? RefreshCallbacAsync { get; set; }

        public SafetyInventoryEditViewModel(ISafetyInventoryAppService safetyInventoryAppService, IServiceProvider serviceProvider)
        {
            _safetyInventoryAppService = safetyInventoryAppService;
            _serviceProvider = serviceProvider;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;

                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _safetyInventoryAppService.GetAsync(id);
                    Model.ProductId = result.ProductId;
                    Model.ProductName = result.ProductName;
                    Model.ProductNumber = result.ProductNumber;
                    Model.ProductSpec = result.ProductSpec;
                    Model.ProductUnitName = result.ProductUnitName;
                    Model.MinQuantity = result.MinQuantity;
                    Model.MaxQuantity = result.MaxQuantity;
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


        [Command]
        public void ShowSelectProdctWindow()
        {
            if (this.WindowService != null)
            {
                ProductSelectAllViewModel? viewModel = _serviceProvider.GetService<ProductSelectAllViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = this.OnProductSelectedCallback;
                    WindowService.Title = "安全库存-选择产品";
                    WindowService.Show("ProductSelectAllView", viewModel);
                }
            }
        }

        [AsyncCommand]
        public async Task SaveAsync()
        {
            await UpdateAsync();
        }


        [Command]
        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }


        private void OnProductSelectedCallback(ProductDto product)
        {
            this.Model.ProductId = product.Id;
            this.Model.ProductNumber = product.Number;
            this.Model.ProductName = product.Name;
            this.Model.ProductSpec = product.Spec;
            this.Model.ProductUnitName = product.ProductUnitName;
        }

        private async Task Create()
        {
            try
            {
                this.IsLoading = true;
                SafetyInventoryCreateDto dto = new SafetyInventoryCreateDto();
                dto.ProductId = Model.ProductId;
                dto.MinQuantity = Model.MinQuantity;
                dto.MaxQuantity = Model.MaxQuantity;
                await _safetyInventoryAppService.CreateAsync(dto);
                if (RefreshCallbacAsync != null)
                {
                    await RefreshCallbacAsync();
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


        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                SafetyInventoryUpdateDto dto = new SafetyInventoryUpdateDto();
                dto.ProductId = Model.ProductId;
                dto.MinQuantity = Model.MinQuantity;
                dto.MaxQuantity = Model.MaxQuantity;
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _safetyInventoryAppService.UpdateAsync((Guid)this.Model.Id, dto);
                if (RefreshCallbacAsync != null)
                {
                    await RefreshCallbacAsync();
                    this.CurrentWindowService.Close();
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
