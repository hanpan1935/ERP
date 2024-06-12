using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Selects;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Edits
{
    public partial class ArrivalNoticeEditViewModel : EditViewModelBase<ArrivalNoticeEditModel>
    {

        private readonly IArrivalNoticeAppService _arrivalNoticeAppService;
        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }
        private readonly IServiceProvider _serviceProvider;

        public ArrivalNoticeEditViewModel(
            IArrivalNoticeAppService arrivalNoticeAppService,
            IServiceProvider serviceProvider
            )
        {
            _arrivalNoticeAppService = arrivalNoticeAppService;
            _serviceProvider = serviceProvider;
            Model.Details = new ObservableCollection<ArrivalNoticeDetailEditModel>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "来料通知-编辑";
                    Guid id = (Guid)Model.Id;
                    var result = await _arrivalNoticeAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.ArrivalTime = result.ArrivalTime;
                    Model.Remark = result.Remark;
                    Model.Details.Clear();
                    foreach (var detail in result.Details)
                    {
                        ArrivalNoticeDetailEditModel detailModel = new ArrivalNoticeDetailEditModel();
                        detailModel.Id = detail.Id;
                        detailModel.PurchaseOrderNumber = detail.PurchaseOrderNumber;
                        detailModel.ProductNumber = detail.ProductNumber;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductSpec = detail.ProductSpec;
                        detailModel.ProductUnitName = detail.ProductUnitName;
                        detailModel.PurchaseOrderDetailId = detail.PurchaseOrderDetailId;
                        detailModel.Quantity = detail.Quantity;
                        Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "来料通知-新建";
                    Model.Number = "系统自动生成";
                    Model.ArrivalTime = DateTime.Now;
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

        [Command]
        public void DeleteSelectedRow()
        {
            if (Model.SelectedRow != null)
            {
                Model.Details.Remove(Model.SelectedRow);
            }
        }

        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null || Model.Id == Guid.Empty)
            {
                await CreateAsync();
            }
            else
            {
                await UpdateAsync();
            }
            await CloseAsync();
        }

        public bool CanSave()
        {
            bool hasError = HasErrors();
            if (hasError == true) return !hasError;
            if (Model.Details.Count == 0)
            {
                return false;
            }
            else
            {
                foreach (var item in Model.Details)
                {
                    if (item.HasErrors() == true) return false;
                }
            }
            return true;
        }

        [Command]
        public void ShowSelectPurchaseOrderWindow()
        {
            PurchaseOrderMultipleSelectDetailViewModel? viewModel = _serviceProvider.GetService<PurchaseOrderMultipleSelectDetailViewModel>();
            if (viewModel != null)
            {
                WindowService.Title = "来料通知-编辑";
                viewModel.SaveCallback = OnPurchaseOrderSelected;
                //arrivalNoticeEditViewModel.i
                WindowService.Show("PurchaseOrderMultipleSelectDetailView", viewModel);
            }

        }

        private void OnPurchaseOrderSelected(ICollection<PurchaseOrderDetailSelectDto> purchaseOrderDetails)
        {
            foreach (var item in purchaseOrderDetails)
            {
                var isExsits = Model.Details.Any(m => m.PurchaseOrderDetailId == item.Id);
                if (isExsits == true) continue;
                ArrivalNoticeDetailEditModel detailEditModel = new ArrivalNoticeDetailEditModel();
                detailEditModel.PurchaseOrderNumber = item.PurchaseOrderNumber;
                detailEditModel.PurchaseOrderDetailQuantity = item.Quantity;
                detailEditModel.ProductNumber = item.ProductNumber;
                detailEditModel.ProductName = item.ProductName;
                detailEditModel.ProductSpec = item.ProductSpec;
                detailEditModel.ProductUnitName = item.ProductUnitName;
                detailEditModel.PurchaseOrderDetailId = item.Id;
                detailEditModel.Quantity = 0;
                Model.Details.Add(detailEditModel);
            }
        }


        [AsyncCommand]
        public async Task CloseAsync()
        {
            if (CurrentWindowService != null)
            {
                CurrentWindowService.Close();
            }
            if (this.OnCloseWindowCallbackAsync != null)
            {
                await OnCloseWindowCallbackAsync();
            }
        }


        /// <summary>
        /// 全部来料
        /// </summary>
        [Command]
        public void AllArrivaled()
        {
            foreach (var item in Model.Details)
            {
                item.Quantity = item.PurchaseOrderDetailQuantity;
            }
        }

        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                ArrivalNoticeCreateDto dto = new ArrivalNoticeCreateDto();
                dto.ArrivalTime = Model.ArrivalTime;
              
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    ArrivalNoticeDetailCreateDto detailDto = new ArrivalNoticeDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.PurchaseOrderDetailId = detail.PurchaseOrderDetailId;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                await _arrivalNoticeAppService.CreateAsync(dto);
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

        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                ArrivalNoticeUpdateDto dto = new ArrivalNoticeUpdateDto();
                dto.ArrivalTime = Model.ArrivalTime;
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    ArrivalNoticeDetailUpdateDto detailDto = new ArrivalNoticeDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.PurchaseOrderDetailId = detail.PurchaseOrderDetailId;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _arrivalNoticeAppService.UpdateAsync((Guid)Model.Id, dto);

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
    }
}

