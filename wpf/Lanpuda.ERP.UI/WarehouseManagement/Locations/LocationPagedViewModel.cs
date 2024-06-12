using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using Lanpuda.ERP.WarehouseManagement.Locations.Edits;
using Lanpuda.ERP.WarehouseManagement.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.Locations
{
    public class LocationPagedViewModel : PagedViewModelBase<LocationDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILocationAppService _locationAppService;
        private readonly IWarehouseAppService _warehouseLookupAppService;
        public ObservableCollection<WarehouseDto> WarehouseSource { get; set; }

        public Guid? WarehouseId
        {
            get { return GetProperty(() => WarehouseId); }
            set { SetProperty(() => WarehouseId, value); }
        }

        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }


        public LocationPagedViewModel(
            IServiceProvider serviceProvider, 
            ILocationAppService locationAppService, 
            IWarehouseAppService warehouseLookupAppService)
        {
            _serviceProvider = serviceProvider;
            _locationAppService = locationAppService;
            _warehouseLookupAppService = warehouseLookupAppService;
            WarehouseSource = new ObservableCollection<WarehouseDto>();

        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var warehouseList = await _warehouseLookupAppService.GetAllAsync();
                this.WarehouseSource.Clear();
                foreach (var item in warehouseList)
                {
                    this.WarehouseSource.Add(item);
                }
                await this.QueryAsync();
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
        public void Create()
        {
            if (this.WindowService != null)
            {
                LocationEditViewModel? viewModel = _serviceProvider.GetService<LocationEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Refresh = this.QueryAsync;
                    WindowService.Title = "库位设置-新建";
                    WindowService.Show("LocationEditView", viewModel);
                }
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null )
            {
                return;
            }

            if (this.WindowService != null)
            {
                LocationEditViewModel? viewModel = _serviceProvider.GetService<LocationEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.Refresh = this.QueryAsync;
                    WindowService.Title = "库位设置-编辑";
                    WindowService.Show("LocationEditView", viewModel);
                }
            }
        }
     


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Name = String.Empty;
            this.WarehouseId = null;
            this.Number = string.Empty;
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
                    await _locationAppService.DeleteAsync(this.SelectedModel.Id);
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

        public bool CanDeleteAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            return true;
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                LocationPagedRequestDto requestDto = new LocationPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = SkipCount;
                requestDto.WarehouseId = this.WarehouseId;
                requestDto.Name = this.Name;
                requestDto.Number = this.Number;
                var result = await _locationAppService.GetPagedListAsync(requestDto);
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
