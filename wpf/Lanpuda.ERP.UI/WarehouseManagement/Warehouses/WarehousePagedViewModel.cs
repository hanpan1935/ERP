using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Edits;
using DevExpress.Mvvm;
using Microsoft.Extensions.DependencyInjection;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses
{
    public class WarehousePagedViewModel : PagedViewModelBase<WarehouseDto>
    {
        private readonly IWarehouseAppService _warehouseAppService;
        private readonly IServiceProvider _serviceProvider;

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



        public WarehousePagedViewModel(IWarehouseAppService warehouseAppService, IServiceProvider serviceProvider)
        {
            _warehouseAppService = warehouseAppService;
            this.PageTitle = "仓库设置";
            _serviceProvider = serviceProvider;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            WarehouseEditViewModel? viewModel = _serviceProvider.GetService<WarehouseEditViewModel>();
            if (viewModel != null)
            {
                viewModel.Refresh = this.QueryAsync;
                WindowService.Title = "仓库设置-新建";
                WindowService.Show("WarehouseEditView", viewModel);
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            WarehouseEditViewModel? viewModel = _serviceProvider.GetService<WarehouseEditViewModel>();
            if (viewModel != null)
            {
                viewModel.Model.Id = this.SelectedModel.Id;
                viewModel.Refresh = this.QueryAsync;
                WindowService.Title = "仓库设置-编辑";
                WindowService.Show("WarehouseEditView", viewModel);
            }
        }

        public bool CanUpdate()
        {

            if (this.SelectedModel == null)
            {
                return false;
            }
            return true;
        }



        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Name = String.Empty;
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
                this.IsLoading = true;
                await _warehouseAppService.DeleteAsync(this.SelectedModel.Id);
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

        public bool CanDelete()
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
                WarehousePagedRequestDto requestDto = new WarehousePagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.Name = this.Name;
                var result = await _warehouseAppService.GetPagedListAsync(requestDto);
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
