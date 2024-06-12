using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies.Dtos;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Mvvm;
using Lanpuda.ERP.SalesManagement.ShipmentApplies.Edits;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies.Edits;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using IdentityModel.Client;
using Lanpuda.Client.Common;
using Lanpuda.Client.Theme.Utils;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies
{
    public class SalesReturnApplyPagedViewModel : PagedViewModelBase<SalesReturnApplyDto>
    {
        private readonly ISalesReturnApplyAppService _salesReturnApplyAppService;
        private readonly IServiceProvider _serviceProvider;

        #region 搜索

        
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }


        public string CustomerName
        {
            get { return GetProperty(() => CustomerName); }
            set { SetProperty(() => CustomerName, value); }
        }

        public SalesReturnReason? SalesReturnReason
        {
            get { return GetProperty(() => SalesReturnReason); }
            set { SetProperty(() => SalesReturnReason, value); }
        }


        public bool? IsProductReturn
        {
            get { return GetProperty(() => IsProductReturn); }
            set { SetProperty(() => IsProductReturn, value); }
        }


        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }

        public Dictionary<string, SalesReturnReason> SalesReturnReasonSource { get; set; }

        public Dictionary<string,bool> IsProductReturnSource { get; set; }

        public Dictionary<string, bool> IsConfirmedSource { get; set; }


        #endregion

        public SalesReturnApplyPagedViewModel(ISalesReturnApplyAppService salesReturnApplyAppService, IServiceProvider serviceProvider)
        {
            _salesReturnApplyAppService = salesReturnApplyAppService;
            _serviceProvider = serviceProvider;
            this.PageTitle = "退货申请";

            SalesReturnReasonSource = EnumUtils.EnumToDictionary<SalesReturnReason>();
            IsProductReturnSource = ItemsSoureUtils.GetBoolDictionary();
            IsConfirmedSource = ItemsSoureUtils.GetBoolDictionary();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            SalesReturnApplyEditViewModel? viewModel = _serviceProvider.GetService<SalesReturnApplyEditViewModel>();
            if (viewModel != null)
            {
                viewModel.RefreshCallbackAsync = this.QueryAsync;
                this.WindowService.Title = "退货申请-新建";
                this.WindowService.Show("SalesReturnApplyEditView", viewModel);
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            SalesReturnApplyEditViewModel? viewModel = _serviceProvider.GetService<SalesReturnApplyEditViewModel>();
            if (viewModel != null)
            {
                viewModel.RefreshCallbackAsync = this.QueryAsync;
                viewModel.Model.Id = this.SelectedModel.Id;
                this.WindowService.Title = "退货申请-新建";
                this.WindowService.Show("SalesReturnApplyEditView", viewModel);
            }
        }


        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsConfirmed == true)
            {
                return false;
            }

            return true;
        }

    


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.CustomerName = string.Empty;
            this.IsProductReturn = null;
            this.SalesReturnReason= null;
            this.IsConfirmed = null;
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
                    await _salesReturnApplyAppService.DeleteAsync(SelectedModel.Id);
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


        [AsyncCommand]
        public async Task ConfirmAsync()
        {
            if (SelectedModel == null)
            {
                return;
            }
            try
            {
                this.IsLoading = true;
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "真的要确认吗?确认后无法修改", caption: "提示!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    await _salesReturnApplyAppService.ConfirmAsync(SelectedModel.Id);
                    await this.QueryAsync();
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                this.IsLoading = false;
            }
        }
        public bool CanConfirmAsync()
        {
            if (SelectedModel == null)
            {
                return false;
            }

            if (SelectedModel.IsConfirmed == true)
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
                SalesReturnApplyPagedRequestDto requestDto = new SalesReturnApplyPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                var result = await _salesReturnApplyAppService.GetPagedListAsync(requestDto);
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
