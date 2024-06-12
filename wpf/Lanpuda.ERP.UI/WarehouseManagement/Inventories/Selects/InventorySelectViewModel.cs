using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.ERP.WarehouseManagement.Inventories.Selects
{
    internal class InventorySelectViewModel : PagedViewModelBase<InventoryDto>
    {
        private readonly IInventoryAppService _inventoryAppService;
        private readonly IServiceProvider _serviceProvider;
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        public Action<InventoryDto>? OnSelectedCallback;

        #region SearchItems
        public Guid? ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId, value); }
        }

        public string? ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        #endregion

        public InventorySelectViewModel(IInventoryAppService inventoryAppService, IServiceProvider serviceProvider)
        {
            _inventoryAppService = inventoryAppService;
            _serviceProvider = serviceProvider;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Selected()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            if (this.OnSelectedCallback != null)
            {
                OnSelectedCallback(this.SelectedModel);
                this.CurrentWindowService.Close();
            }
        }




        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                InventoryPagedRequestDto requestDto = new InventoryPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.ProductId = this.ProductId;
                requestDto.LocationId = null;
                requestDto.WarehouseId = null;
                requestDto.Batch = null;

                var result = await _inventoryAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.Clear();
                PagedDatas.CanNotify = false;
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                PagedDatas.CanNotify = true;
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
