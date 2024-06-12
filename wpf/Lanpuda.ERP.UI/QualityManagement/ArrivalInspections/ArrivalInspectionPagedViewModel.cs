using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.ObjectMapping;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using System.Collections.ObjectModel;
using Lanpuda.ERP.QualityManagement.ArrivalInspections.Edits;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices;
using Lanpuda.ERP.QualityManagement.ArrivalInspections.Dtos;
using System.Collections.Generic;
using Lanpuda.Client.Theme.Utils;
using Lanpuda.ERP.BasicData.Products;
using DevExpress.Mvvm.POCO;

namespace Lanpuda.ERP.QualityManagement.ArrivalInspections
{
    public class ArrivalInspectionPagedViewModel : PagedViewModelBase<ArrivalInspectionDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IObjectMapper _objectMapper;
        private readonly ISupplierAppService _supplierLookupAppService;
        private readonly IArrivalInspectionAppService _arrivalInspectionAppService;

        #region 搜索
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string ArrivalNoticeNumber
        {
            get { return GetProperty(() => ArrivalNoticeNumber); }
            set { SetProperty(() => ArrivalNoticeNumber, value); }
        }

        public string PurchaseStorageNumber
        {
            get { return GetProperty(() => PurchaseStorageNumber); }
            set { SetProperty(() => PurchaseStorageNumber, value); }
        }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }

        public Dictionary<string,bool> IsConfirmedSource { get; set; }
        #endregion

        public ArrivalInspectionPagedViewModel(
                IServiceProvider serviceProvider,
                IObjectMapper objectMapper,
                ISupplierAppService supplierLookupAppService,
                IArrivalInspectionAppService arrivalInspectionAppService
                )
        {
            _serviceProvider = serviceProvider;
            _supplierLookupAppService = supplierLookupAppService;
            _arrivalInspectionAppService = arrivalInspectionAppService;
            this.PageTitle = "来料检验";
            _objectMapper = objectMapper;
            IsConfirmedSource = ItemsSoureUtils.GetBoolDictionary();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
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
        public void Update()
        {
            if (this.WindowService != null)
            {
                ArrivalInspectionEditViewModel? viewModel = _serviceProvider.GetService<ArrivalInspectionEditViewModel>();
                if (viewModel != null && SelectedModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.Refresh = this.QueryAsync;
                    WindowService.Title = "来料检验-编辑";
                    WindowService.Show("ArrivalInspectionEditView", viewModel);
                }
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
            this.Number = string.Empty;
            this.ArrivalNoticeNumber= string.Empty;
            this.PurchaseStorageNumber = string.Empty;
            this.IsConfirmed = null; 
            this.ProductName = string.Empty;
            await QueryAsync();
        }


        [AsyncCommand]
        public async Task ConfirmeAsync()
        {
            if (this.SelectedModel == null) return;
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确认后无法修改", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await this._arrivalInspectionAppService.ConfirmeAsync(this.SelectedModel.Id);
                    await this.QueryAsync();
                }
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
            return true;
        }



        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                ArrivalInspectionPagedRequestDto requestDto = new ArrivalInspectionPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount; 
                requestDto.Number = this.Number;
                requestDto.ArrivalNoticeNumber= this.ArrivalNoticeNumber;
                requestDto.PurchaseStorageNumber= this.PurchaseStorageNumber;
                requestDto.IsConfirmed = this.IsConfirmed;
                requestDto.ProductName = this.ProductName;
                var result = await _arrivalInspectionAppService.GetPagedListAsync(requestDto);
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
