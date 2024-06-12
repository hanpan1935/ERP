using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.OtherStorages.Dtos;
using Lanpuda.ERP.WarehouseManagement.OtherStorages.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.ObjectMapping;


namespace Lanpuda.ERP.WarehouseManagement.OtherStorages
{
    public class OtherStoragePagedViewModel : PagedViewModelBase<OtherStorageDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IOtherStorageAppService _otherStorageAppService;
        private readonly IObjectMapper _objectMapper;


        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }


        public bool? IsSuccessful
        {
            get { return GetProperty(() => IsSuccessful); }
            set { SetProperty(() => IsSuccessful, value); }
        }

        public Dictionary<string, bool> IsSuccessfulSource { get; set; }


        public OtherStoragePagedViewModel(
            IServiceProvider serviceProvider,
            IOtherStorageAppService purchaseReturnApplyAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _otherStorageAppService = purchaseReturnApplyAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "其他入库";
            IsSuccessfulSource = new Dictionary<string, bool>();
            IsSuccessfulSource.Add("是", true);
            IsSuccessfulSource.Add("否", false);
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }

        [Command]
        public void Create()
        {
            OtherStorageEditViewModel? otherStorageEditViewModel = _serviceProvider.GetService<OtherStorageEditViewModel>();
            if (otherStorageEditViewModel != null)
            {
                WindowService.Title = "其他入库-新建";
                otherStorageEditViewModel.RefreshCallbackAsync = QueryAsync;
                WindowService.Show("OtherStorageEditView", otherStorageEditViewModel);
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            OtherStorageEditViewModel? otherStorageEditViewModel = _serviceProvider.GetService<OtherStorageEditViewModel>();
            if (otherStorageEditViewModel != null)
            {
                WindowService.Title = "其他入库-编辑";
                otherStorageEditViewModel.RefreshCallbackAsync = QueryAsync;
                otherStorageEditViewModel.Model.Id = SelectedModel.Id;
                WindowService.Show("OtherStorageEditView", otherStorageEditViewModel);
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
            this.IsSuccessful = null;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task StoragedAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                this.IsLoading = true;
                await _otherStorageAppService.StoragedAsync(this.SelectedModel.Id);
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

        public bool CanStoragedAsync()
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
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _otherStorageAppService.DeleteAsync(SelectedModel.Id);
                    await this.QueryAsync();
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
                OtherStoragePagedRequestDto requestDto = new OtherStoragePagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.IsSuccessful = this.IsSuccessful;
                var result = await _otherStorageAppService.GetPagedListAsync(requestDto);
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
