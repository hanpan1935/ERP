using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Utils;
using Lanpuda.ERP.WarehouseManagement.InventoryMoves.Dtos;
using Lanpuda.ERP.WarehouseManagement.InventoryMoves.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;


namespace Lanpuda.ERP.WarehouseManagement.InventoryMoves
{
    public class InventoryMovePagedViewModel : PagedViewModelBase<InventoryMoveDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IInventoryMoveAppService _inventoryMoveAppService;
        private readonly IObjectMapper _objectMapper;


        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }
        public string Reason
        {
            get { return GetProperty(() => Reason); }
            set { SetProperty(() => Reason, value); }
        }
        public bool? IsSuccessful
        {
            get { return GetProperty(() => IsSuccessful); }
            set { SetProperty(() => IsSuccessful, value); }
        }

        public Dictionary<string, bool> IsSuccessfulSource { get; set; }




        public InventoryMovePagedViewModel(
            IServiceProvider serviceProvider,
            IInventoryMoveAppService inventoryMoveAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _inventoryMoveAppService = inventoryMoveAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "库存调拨";
            IsSuccessfulSource = ItemsSoureUtils.GetBoolDictionary();
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            InventoryMoveEditViewModel? inventoryMoveEditViewModel = _serviceProvider.GetService<InventoryMoveEditViewModel>();
            if (inventoryMoveEditViewModel != null)
            {
                WindowService.Title = "库存调拨-新建";
                inventoryMoveEditViewModel.RefreshCallbackAsync = QueryAsync;
                WindowService.Show("InventoryMoveEditView", inventoryMoveEditViewModel);
            }
        }



        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            InventoryMoveEditViewModel? inventoryMoveEditViewModel = _serviceProvider.GetService<InventoryMoveEditViewModel>();
            if (inventoryMoveEditViewModel != null)
            {
                WindowService.Title = "库存调拨-编辑";
                inventoryMoveEditViewModel.RefreshCallbackAsync = QueryAsync;
                inventoryMoveEditViewModel.Model.Id = SelectedModel.Id;
                WindowService.Show("InventoryMoveEditView", inventoryMoveEditViewModel);
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
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task OutedAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                this.IsLoading = true;
                await _inventoryMoveAppService.MovedAsync(this.SelectedModel.Id);
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

        public bool CanOutedAsync()
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
                await _inventoryMoveAppService.DeleteAsync(this.SelectedModel.Id);
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
                InventoryMovePagedRequestDto requestDto = new InventoryMovePagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                var result = await _inventoryMoveAppService.GetPagedListAsync(requestDto);
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
