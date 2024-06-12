using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Utils;
using Lanpuda.ERP.WarehouseManagement.InventoryChecks.Dtos;
using Lanpuda.ERP.WarehouseManagement.InventoryChecks.Edits;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;


namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks
{
    public class InventoryCheckPagedViewModel : PagedViewModelBase<InventoryCheckDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IInventoryCheckAppService _inventoryCheckAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IWarehouseAppService _warehouseAppService;


        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public DateTime? CheckDate
        {
            get { return GetProperty(() => CheckDate); }
            set { SetProperty(() => CheckDate, value); }
        }


        public Guid? WarehouseId
        {
            get { return GetProperty(() => WarehouseId); }
            set { SetProperty(() => WarehouseId, value); }
        }


        public bool? IsSuccessful
        {
            get { return GetProperty(() => IsSuccessful); }
            set { SetProperty(() => IsSuccessful, value); }
        }

        public ObservableCollection<WarehouseDto> WarehouseSource { get; set; }


        public Dictionary<string, bool> IsSuccessfulSource { get; set; }


        public InventoryCheckPagedViewModel(
            IServiceProvider serviceProvider,
            IInventoryCheckAppService inventoryCheckAppService,
            IWarehouseAppService warehouseAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _inventoryCheckAppService = inventoryCheckAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "库存盘点";
            WarehouseSource = new ObservableCollection<WarehouseDto>();
            IsSuccessfulSource = ItemsSoureUtils.GetBoolDictionary();
            _warehouseAppService = warehouseAppService;
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var warehouseList = await this._warehouseAppService.GetAllAsync();
                this.WarehouseSource.Clear();
                foreach (var item in warehouseList)
                {
                    this.WarehouseSource.Add(item);
                }
                await this.QueryAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
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
            InventoryCheckEditViewModel? inventoryCheckEditViewModel = _serviceProvider.GetService<InventoryCheckEditViewModel>();
            if (inventoryCheckEditViewModel != null)
            {
                WindowService.Title = "库存盘点-新建";
                inventoryCheckEditViewModel.RefreshCallbackAsync = QueryAsync;
                WindowService.Show("InventoryCheckEditView", inventoryCheckEditViewModel);
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            InventoryCheckEditViewModel? inventoryCheckEditViewModel = _serviceProvider.GetService<InventoryCheckEditViewModel>();
            if (inventoryCheckEditViewModel != null)
            {
                WindowService.Title = "库存盘点-编辑";
                inventoryCheckEditViewModel.RefreshCallbackAsync = QueryAsync;
                inventoryCheckEditViewModel.Model.Id = SelectedModel.Id;
                WindowService.Show("InventoryCheckEditView", inventoryCheckEditViewModel);
            }
        }


        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }

            if (this.SelectedModel.IsSuccessful != false)
            {
                return false;
            }
            return true;
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.CheckDate = null;
            this.WarehouseId = null;
            this.IsSuccessful = null;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task ConfirmeAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                this.IsLoading = true;
                await _inventoryCheckAppService.ConfirmedAsync(this.SelectedModel.Id);
                await this.QueryAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        public bool CanConfirmeAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsSuccessful == true)
            {
                return false;
            }
            return true;
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
                this.IsLoading = true;
                await _inventoryCheckAppService.DeleteAsync(this.SelectedModel.Id);
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

        public bool CanDeleteAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsSuccessful == true)
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
                InventoryCheckPagedRequestDto requestDto = new InventoryCheckPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.CheckDate = this.CheckDate;
                requestDto.IsSuccessful = this.IsSuccessful;
                requestDto.WarehouseId = this.WarehouseId;
                var result = await _inventoryCheckAppService.GetPagedListAsync(requestDto);
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
