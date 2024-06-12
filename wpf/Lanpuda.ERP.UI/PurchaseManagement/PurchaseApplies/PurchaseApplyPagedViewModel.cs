using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Edits;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.ObjectMapping;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Mvvm;
using Lanpuda.Client.Common;
using Lanpuda.Client.Theme.Utils;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseApplies
{
    public class PurchaseApplyPagedViewModel : PagedViewModelBase<PurchaseApplyDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPurchaseApplyAppService _purchaseApplyAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly ISupplierAppService _supplierLookupAppService;

        #region 搜索

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public Guid? MrpId
        {
            get { return GetProperty(() => MrpId); }
            set { SetProperty(() => MrpId, value); }
        }

        public string MrpNumber
        {
            get { return GetProperty(() => MrpNumber); }
            set { SetProperty(() => MrpNumber, value); }
        }

        public PurchaseApplyType? ApplyType
        {
            get { return GetProperty(() => ApplyType); }
            set { SetProperty(() => ApplyType, value); }
        }

        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }

        public bool? IsAccept
        {
            get { return GetProperty(() => IsAccept); }
            set { SetProperty(() => IsAccept, value); }
        }

        public Dictionary<string, PurchaseApplyType> ApplyTypeSource { get; set; }
        public Dictionary<string, bool> IsConfirmedSource { get; set; }
        public Dictionary<string, bool> IsAcceptSource { get; set; }

        #endregion



        public PurchaseApplyPagedViewModel(
            IServiceProvider serviceProvider,
            IPurchaseApplyAppService purchaseApplyAppService,
            IObjectMapper objectMapper,
            ISupplierAppService supplierLookupAppService
            )
        {
            _serviceProvider = serviceProvider;
            _purchaseApplyAppService = purchaseApplyAppService;
            _objectMapper = objectMapper;
            _supplierLookupAppService = supplierLookupAppService;
            ApplyTypeSource = EnumUtils.EnumToDictionary<PurchaseApplyType>();
            IsConfirmedSource = ItemsSoureUtils.GetBoolDictionary();
            IsAcceptSource = ItemsSoureUtils.GetBoolDictionary();
            this.PageTitle = "采购申请";
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
        public void Create()
        {

            PurchaseApplyEditViewModel? purchaseApplyEditViewModel = _serviceProvider.GetService<PurchaseApplyEditViewModel>();
            if (purchaseApplyEditViewModel != null)
            {
                WindowService.Title = "采购订单-新建";
                purchaseApplyEditViewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                WindowService.Show("PurchaseApplyEditView", purchaseApplyEditViewModel);
            }

        }

        [Command]
        public void Update()
        {
            if (this.SelectedModel == null) return;
            PurchaseApplyEditViewModel? purchaseApplyEditViewModel = _serviceProvider.GetService<PurchaseApplyEditViewModel>();
            if (purchaseApplyEditViewModel != null)
            {
                WindowService.Title = "采购订单-编辑";
                purchaseApplyEditViewModel.Model.Id = this.SelectedModel.Id;
                purchaseApplyEditViewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                WindowService.Show("PurchaseApplyEditView", purchaseApplyEditViewModel);
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
                    await _purchaseApplyAppService.DeleteAsync(this.SelectedModel.Id);
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
        public async Task AcceptAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要接收吗?接收后会根据采购报价自动创建采购订单", caption: "警告!", button: MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    this.IsLoading = true;
                    await _purchaseApplyAppService.AcceptAsync(this.SelectedModel.Id);
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

        public bool CanAcceptAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsConfirmed == false)
            {
                return false;
            }
            if (this.SelectedModel.IsAccept == true)
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
                        await _purchaseApplyAppService.ConfirmeAsync(id);
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
                PurchaseApplyGetListInput requestDto = new PurchaseApplyGetListInput();
                requestDto.Number = this.Number;
                requestDto.MrpId = this.MrpId;
                requestDto.MrpNumber = this.MrpNumber;
                requestDto.ApplyType = this.ApplyType;
                requestDto.IsConfirmed = this.IsConfirmed;
                requestDto.IsAccept = this.IsAccept;
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                var result = await _purchaseApplyAppService.GetPagedListAsync(requestDto);
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
