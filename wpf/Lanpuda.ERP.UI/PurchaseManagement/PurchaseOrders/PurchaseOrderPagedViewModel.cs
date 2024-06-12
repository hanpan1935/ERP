using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Edits;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders
{
    public class PurchaseOrderPagedViewModel : PagedViewModelBase<PurchaseOrderDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPurchaseOrderAppService _purchaseOrderAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly ISupplierAppService _supplierLookupAppService;

    
        public PurchaseOrderPagedRequestModel RequestModel
        {
            get { return GetProperty(() => RequestModel); }
            set { SetProperty(() => RequestModel, value); }
        }
       

        public PurchaseOrderPagedViewModel(
            IServiceProvider serviceProvider,
            IPurchaseOrderAppService purchaseOrderAppService,
            IObjectMapper objectMapper,
            ISupplierAppService supplierLookupAppService
            )
        {
            _serviceProvider = serviceProvider;
            _purchaseOrderAppService = purchaseOrderAppService;
            _objectMapper = objectMapper;
            _supplierLookupAppService = supplierLookupAppService;
            RequestModel = new PurchaseOrderPagedRequestModel();
            this.PageTitle = "采购订单";
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var suppliers = await _supplierLookupAppService.GetAllAsync();
                this.RequestModel._dataList = suppliers;
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

            PurchaseOrderEditViewModel? purchaseOrderEditViewModel = _serviceProvider.GetService<PurchaseOrderEditViewModel>();
            if (purchaseOrderEditViewModel != null)
            {
                WindowService.Title = "采购订单-新建";
                purchaseOrderEditViewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                WindowService.Show("PurchaseOrderEditView", purchaseOrderEditViewModel);
            }

        }

        [Command]
        public void Update()
        {
            if (this.SelectedModel == null) return;
            PurchaseOrderEditViewModel? purchaseOrderEditViewModel = _serviceProvider.GetService<PurchaseOrderEditViewModel>();
            if (purchaseOrderEditViewModel != null)
            {
                WindowService.Title = "采购订单-编辑";
                purchaseOrderEditViewModel.Model.Id = this.SelectedModel.Id;
                purchaseOrderEditViewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                WindowService.Show("PurchaseOrderEditView", purchaseOrderEditViewModel);
            }
        }

        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (SelectedModel.IsConfirmed != false)
            {
                return false;
            }
            return true;
        }



        [AsyncCommand]
        public async Task ResetAsync()
        {
            RequestModel.Number = String.Empty;
            RequestModel.SupplierId = null;
            RequestModel.RequiredDateStart = null;
            RequestModel.RequiredDateEnd = null;
            RequestModel.IsConfirmed = null;
            RequestModel.OrderType = null;
            RequestModel.CloseStatus = null;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            if (this.SelectedModel == null) return;
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _purchaseOrderAppService.DeleteAsync(this.SelectedModel.Id);
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
            if (SelectedModel == null)
            {
                return false;
            }
            return true;
        }

        [AsyncCommand]
        public async Task CloseOrderAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要关闭吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    this.IsLoading = true;
                    await _purchaseOrderAppService.CloseAsync(this.SelectedModel.Id);
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
        }

        public bool CanCloseOrderAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsConfirmed == false)
            {
                return false;
            }
            if (this.SelectedModel.CloseStatus != PurchaseOrderCloseStatus.Opened )
            {
                return false;
            }
            return true;
        }


        [AsyncCommand]
        public async Task ConfirmeAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确认后无法修改", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        this.IsLoading = true;
                        Guid id = this.SelectedModel.Id;
                        await _purchaseOrderAppService.ConfirmeAsync(id);
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
            if (this.SelectedModel.IsConfirmed != false)
            {
                return false;
            }
            if (this.SelectedModel.Details.Count == 0)
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
                PurchaseOrderPagedRequestDto requestDto = new PurchaseOrderPagedRequestDto();
                requestDto.Number = RequestModel.Number;
                requestDto.SupplierId = RequestModel.SupplierId;
                requestDto.RequiredDateStart = RequestModel.RequiredDateStart;
                requestDto.RequiredDateEnd = RequestModel.RequiredDateEnd;
                requestDto.IsConfirmed = RequestModel.IsConfirmed;
                requestDto.OrderType = RequestModel.OrderType;
                requestDto.CloseStatus = RequestModel.CloseStatus;
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                var result = await _purchaseOrderAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
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
