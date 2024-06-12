using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelect;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories.BulkCreates
{
    public class SafetyInventoryBulkCreateViewModel : EditViewModelBase<ObservableCollection<SafetyInventoryBulkCreateModel>>
    {
        private readonly ISafetyInventoryAppService _safetyInventoryAppService;
        private readonly IServiceProvider _serviceProvider;

        public Func<Task>? Refresh { get; set; }
        public SafetyInventoryBulkCreateModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        public SafetyInventoryBulkCreateViewModel(
            ISafetyInventoryAppService safetyInventoryAppService,
            IServiceProvider serviceProvider
            )
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
                await Task.Delay(100);
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
                ProductMultipleSelectViewModel? viewModel = _serviceProvider.GetService<ProductMultipleSelectViewModel>();
                if (viewModel != null)
                {
                    viewModel.SaveCallback = this.OnProductSelectedCallback;
                    WindowService.Title = "安全库存-选择产品";
                    WindowService.Show("ProductMultipleSelectView", viewModel);
                }
            }
        }

        [Command]
        public void DeleteSelectedRow()
        {
            if (this.SelectedRow == null)
            {
                return;
            }
            this.Model.Remove(this.SelectedRow);
            
        }


        [AsyncCommand]
        public async Task SaveAsync()
        {
            await CreateAsync();
        }

        public bool CanSaveAsync()
        {
            foreach (var item in Model)
            {
                bool hasError = item.HasErrors();
                return !hasError;
            }
            return true;
        }


        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
               

                List<SafetyInventoryCreateDto> list = new List<SafetyInventoryCreateDto>();
                foreach (var item in Model)
                {
                    SafetyInventoryCreateDto dto = new SafetyInventoryCreateDto();
                    dto.ProductId = item.ProductId;
                    dto.MinQuantity = item.MinQuantity;
                    dto.MaxQuantity = item.MaxQuantity;
                    list.Add(dto);
                }
                await _safetyInventoryAppService.BulkCreateAsync(list);
                if (Refresh != null)
                {
                    await Refresh();
                    this.CurrentWindowService.Close();
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


        private void OnProductSelectedCallback(ICollection<ProductDto> products)
        {
            foreach (var item in products)
            {
                SafetyInventoryBulkCreateModel createModel = new SafetyInventoryBulkCreateModel();
                createModel.ProductId = item.Id;
                createModel.ProductName = item.Name;
                createModel.ProductNumber = item.Number;
                createModel.ProductSpec = item.Spec;
                createModel.ProductUnitName = item.ProductUnitName;

                this.Model.Add(createModel);
            }
        }
    }
}
