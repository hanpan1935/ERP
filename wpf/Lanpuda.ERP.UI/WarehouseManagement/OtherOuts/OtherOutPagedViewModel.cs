using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using Volo.Abp.ObjectMapping;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.ERP.WarehouseManagement.OtherOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.OtherOuts;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Edits;
using Lanpuda.ERP.WarehouseManagement.OtherOuts.Edits;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts
{
    public class OtherOutPagedViewModel : PagedViewModelBase<OtherOutDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IOtherOutAppService _otherOutAppService;
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

        public Dictionary<string,bool> IsSuccessfulSource { get; set; }


        public OtherOutPagedViewModel(
            IServiceProvider serviceProvider,
            IOtherOutAppService otherOutAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _otherOutAppService = otherOutAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "其他出库";
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
            OtherOutEditViewModel? otherOutEditViewModel = _serviceProvider.GetService<OtherOutEditViewModel>();
            if (otherOutEditViewModel != null)
            {
                WindowService.Title = "其他出库-新建";
                otherOutEditViewModel.RefreshCallbackAsync = QueryAsync;
                WindowService.Show("OtherOutEditView", otherOutEditViewModel);
            }
        }



        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            OtherOutEditViewModel? otherOutEditViewModel = _serviceProvider.GetService<OtherOutEditViewModel>();
            if (otherOutEditViewModel != null)
            {
                WindowService.Title = "采购退货-编辑";
                otherOutEditViewModel.RefreshCallbackAsync = QueryAsync;
                otherOutEditViewModel.Model.Id = SelectedModel.Id;
                WindowService.Show("OtherOutEditView", otherOutEditViewModel);
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
        public async Task OutedAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                this.IsLoading = true;
                await _otherOutAppService.OutedAsync(this.SelectedModel.Id);
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

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                OtherOutPagedRequestDto requestDto = new OtherOutPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.IsSuccessful= this.IsSuccessful;
                requestDto.Number = this.Number;
                var result = await _otherOutAppService.GetPagedListAsync(requestDto);
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
