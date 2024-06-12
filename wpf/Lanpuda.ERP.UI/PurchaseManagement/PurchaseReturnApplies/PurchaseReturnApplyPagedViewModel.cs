using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Edits;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Volo.Abp.ObjectMapping;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Edits;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies
{
    public class PurchaseReturnApplyPagedViewModel : PagedViewModelBase<PurchaseReturnApplyDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPurchaseReturnApplyAppService _purchaseReturnApplyAppService;
        private readonly IObjectMapper _objectMapper;


        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string SupplierName
        {
            get { return GetProperty(() => SupplierName); }
            set { SetProperty(() => SupplierName, value); }
        }




        public PurchaseReturnApplyPagedViewModel(
            IServiceProvider serviceProvider, 
            IPurchaseReturnApplyAppService purchaseReturnApplyAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _purchaseReturnApplyAppService = purchaseReturnApplyAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "退货申请";
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            PurchaseReturnApplyEditViewModel? purchaseReturnApplyEditViewModel = _serviceProvider.GetService<PurchaseReturnApplyEditViewModel>();
            if (purchaseReturnApplyEditViewModel != null)
            {
                WindowService.Title = "退货申请-新建";
                purchaseReturnApplyEditViewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                WindowService.Show("PurchaseReturnApplyEditView", purchaseReturnApplyEditViewModel);
            }
        }

        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            PurchaseReturnApplyEditViewModel? purchaseReturnApplyEditViewModel = _serviceProvider.GetService<PurchaseReturnApplyEditViewModel>();
            if (purchaseReturnApplyEditViewModel != null)
            {
                WindowService.Title = "退货申请-编辑";
                purchaseReturnApplyEditViewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                purchaseReturnApplyEditViewModel.Model.Id = this.SelectedModel.Id;
                WindowService.Show("PurchaseReturnApplyEditView", purchaseReturnApplyEditViewModel);
            }
        }

        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }

            if (this.SelectedModel .IsConfirmed != false)
            {
                return false;
            }
            return true;
        }

       

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.SupplierName = string.Empty;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _purchaseReturnApplyAppService.DeleteAsync(this.SelectedModel.Id);
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
  


        [AsyncCommand]
        public async Task ConfirmAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确认后将无法修改！", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _purchaseReturnApplyAppService.ConfirmAsync(this.SelectedModel.Id);
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

        public bool CanConfirmAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsConfirmed != false)
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
                PurchaseReturnApplyPagedRequestDto requestDto = new PurchaseReturnApplyPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                var result = await _purchaseReturnApplyAppService.GetPagedListAsync(requestDto);
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
