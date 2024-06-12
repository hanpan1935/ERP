using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using Lanpuda.ERP.SalesManagement.Customers;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies.Dtos;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies.Edits;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.ERP.SalesManagement.ShipmentApplies.Edits;
using DevExpress.Mvvm;
using Lanpuda.ERP.SalesManagement.SalesOrders.Selects;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;
using Lanpuda.ERP.WarehouseManagement.SalesOuts.Selects;
using Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesReturnApplies.Edits
{
    public class SalesReturnApplyEditViewModel : EditViewModelBase<SalesReturnApplyEditModel>
    {
        private readonly ISalesReturnApplyAppService _salesReturnApplyAppService;
        private readonly ICustomerAppService _customerLookupAppService;
        private readonly IServiceProvider _serviceProvider;
        public Func<Task>? RefreshCallbackAsync { get; set; }

        public SalesReturnApplyDetailEditModel? SelectedDetailEntity { get; set; }

      
        public SalesReturnApplyEditViewModel(
            ISalesReturnApplyAppService salesReturnApplyAppService,
            ICustomerAppService customerLookupAppService,
            IServiceProvider serviceProvider
            )
        {
            _salesReturnApplyAppService = salesReturnApplyAppService;
            _customerLookupAppService = customerLookupAppService;
            _serviceProvider = serviceProvider;
         
        }
    

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var customers = await _customerLookupAppService.GetAllAsync();
                foreach (var item in customers)
                {
                    Model.CustomerSource.Add(item);
                }

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "退货申请-编辑";
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _salesReturnApplyAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.CustomerId = result.CustomerId;
                    Model.Reason= result.Reason;
                    Model.IsProductReturn = result.IsProductReturn;
                    Model.Description = result.Description;
                    foreach (var detail in result.Details)
                    {
                        SalesReturnApplyDetailEditModel detailModel = new SalesReturnApplyDetailEditModel();
                        detailModel.SalesOutDetailId = detail.SalesOutDetailId;
                        detailModel.Quantity = detail.Quantity;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.OutQuantity = detail.OutQuantity;
                        detailModel.Batch = detail.Batch;
                        detailModel.Description= detail.Description;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "退货申请-新建";
                    this.Model.Number = "系统自动生成";
                    Model.IsProductReturn = true;
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
            if (SelectedDetailEntity != null)
            {
                this.Model.Details.Remove(SelectedDetailEntity);
            }
        }

        [Command]
        public void ShowSelectOrderWindow()
        {
            SalesOutDetailMultipleSelectViewModel? viewModel = _serviceProvider.GetService<SalesOutDetailMultipleSelectViewModel>();
            if (viewModel != null)
            {
                viewModel.SaveCallback = this.OnSelected;
                viewModel.CustomerId = this.Model.CustomerId;
                this.WindowService.Title = "选择销售出库单---" +this.Model.CustomerShortName;
                this.WindowService.Show("SalesOutDetailMultipleSelectView", viewModel);
            }
        }
        public bool CanShowSelectOrderWindow()
        {
            if (this.Model.CustomerId == Guid.Empty)
            {
                return false;
            }
            return true;
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
        }

        [Command]
        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }

        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                SalesReturnApplyCreateDto dto = new SalesReturnApplyCreateDto();
                dto.CustomerId = Model.CustomerId;
                dto.Reason = Model.Reason;
                dto.IsProductReturn = Model.IsProductReturn;
                dto.Description = Model.Description;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    SalesReturnApplyDetailCreateDto detailDto = new SalesReturnApplyDetailCreateDto();
                    var item = Model.Details[i];
                    detailDto.SalesOutDetailId = item.SalesOutDetailId;
                    detailDto.Quantity = item.Quantity;
                    detailDto.Description = item.Description;
                    dto.Details.Add(detailDto);
                }
                await _salesReturnApplyAppService.CreateAsync(dto);
                if (this.RefreshCallbackAsync != null)
                {
                    this.Close();
                    await RefreshCallbackAsync();
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

        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                SalesReturnApplyUpdateDto dto = new SalesReturnApplyUpdateDto();
                dto.CustomerId = Model.CustomerId;
                dto.Reason = Model.Reason;
                dto.IsProductReturn = Model.IsProductReturn;
                dto.Description = Model.Description;

                for (int i = 0; i < Model.Details.Count; i++)
                {
                    SalesReturnApplyDetailUpdateDto detailDto = new SalesReturnApplyDetailUpdateDto();
                    var item = Model.Details[i];
                    detailDto.SalesOutDetailId = item.SalesOutDetailId;
                    detailDto.Quantity = item.Quantity;
                    detailDto.Description = item.Description;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _salesReturnApplyAppService.UpdateAsync((Guid)Model.Id, dto);
                if (this.RefreshCallbackAsync != null)
                {
                    this.Close();
                    await RefreshCallbackAsync();
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

        private void OnSelected(ICollection<SalesOutDetailSelectDto> details)
        {
            foreach (var item in details)
            {
                var hasSame = this.Model.Details.Any(m => m.SalesOutDetailId == item.Id);

                if (hasSame) { continue; }

                SalesReturnApplyDetailEditModel detailEditModel = new SalesReturnApplyDetailEditModel();
                detailEditModel.SalesOutDetailId = item.Id;
                detailEditModel.Quantity = 0;
                detailEditModel.OutQuantity = item.OutQuantity;
                detailEditModel.ProductName = item.ProductName;
                detailEditModel.Batch = item.Batch;
                this.Model.Details.Add(detailEditModel);
            }
        }
    }
}

