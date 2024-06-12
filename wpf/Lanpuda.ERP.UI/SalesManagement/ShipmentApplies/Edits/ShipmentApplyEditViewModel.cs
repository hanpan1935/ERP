using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Collections;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.SalesManagement.Customers;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;
using Lanpuda.ERP.SalesManagement.SalesOrders.Selects;
using Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Lanpuda.ERP.SalesManagement.ShipmentApplies.Edits
{
    public class ShipmentApplyEditViewModel : EditViewModelBase<ShipmentApplyEditModel>
    {
        private readonly IShipmentApplyAppService _shipmentApplyAppService;
        private readonly ICustomerAppService _customerAppService;
        private readonly IServiceProvider _serviceProvider;

       
        public Func<Task>? RefreshCallbackAsync { get; set; }

       


        public ShipmentApplyEditViewModel(
            IShipmentApplyAppService shipmentApplyAppService,
            ICustomerAppService customerAppService,
            IServiceProvider serviceProvider
            )
        {
            _shipmentApplyAppService = shipmentApplyAppService;
            _customerAppService = customerAppService;
            _serviceProvider = serviceProvider;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                this.Model.customerList = await _customerAppService.GetAllAsync();

                this.Model.CustomerSource.CanNotify = false;
                foreach (var item in Model.customerList)
                {
                    Model.CustomerSource.Add(item);
                }
                this.Model.CustomerSource.CanNotify = true;
                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "发货申请-编辑";
                    Guid id = (Guid)Model.Id;
                    var result = await _shipmentApplyAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.CustomerId = result.CustomerId;
                    Model.Consignee = result.Consignee;
                    Model.ConsigneeTel = result.ConsigneeTel;
                    Model.Details.Clear();
                    foreach (var item in result.Details)
                    {
                        ShipmentApplyDetailEditModel detailModel = new ShipmentApplyDetailEditModel();
                        detailModel.SalesOrderDetailId = item.SalesOrderDetailId;
                        detailModel.SalesOrderNumber = item.SalesOrderNumber;
                        detailModel.Quantity = item.Quantity;
                        detailModel.OrderQuantity = item.Quantity;
                        detailModel.DeliveryDate = item.DeliveryDate;
                        detailModel.ProductName = item.ProductName;
                        detailModel.ProductNumber = item.ProductNumber;
                        detailModel.ProductUnitName = item.ProductUnitName;
                        detailModel.ProductSpec = item.ProductSpec;
                        detailModel.Requirement = item.Requirement;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "发货申请-新建";
                    Model.Number = "系统自动生成";
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
                this.Model.Details.Remove(Model.SelectedRow);
            }
        }

        [Command]
        public void ShowSelectOrderWindow()
        {
            SalesOrderDetailMultipleSelectViewModel? viewModel = _serviceProvider.GetService<SalesOrderDetailMultipleSelectViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSaveCallback = this.OnSalesOrderSelected;
                viewModel.CustomerId = this.Model.CustomerId;
                viewModel.IsConfirmed = true;
                viewModel.CloseStatus = SalesOrders.SalesOrderCloseStatus.ToBeClosed;
                this.WindowService.Title = "选择销售订单-" + this.Model.CustomerName;
                this.WindowService.Show("SalesOrderDetailMultipleSelectView", viewModel);
            }
        }

        public bool CanShowSelectOrderWindow()
        {
            if (Model.CustomerId == Guid.Empty)
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
                ShipmentApplyCreateDto dto = new ShipmentApplyCreateDto();
                dto.CustomerId = Model.CustomerId;
                dto.Address = Model.ShippingAddress;
                dto.Consignee = Model.Consignee;
                dto.ConsigneeTel = Model.ConsigneeTel;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    ShipmentApplyDetailCreateDto detailDto = new ShipmentApplyDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.SalesOrderDetailId = detail.SalesOrderDetailId;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                await _shipmentApplyAppService.CreateAsync(dto);
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
                ShipmentApplyUpdateDto dto = new ShipmentApplyUpdateDto();
                dto.CustomerId = Model.CustomerId;
                dto.Address = Model.ShippingAddress;
                dto.Consignee = Model.Consignee;
                dto.ConsigneeTel = Model.ConsigneeTel;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    ShipmentApplyDetailUpdateDto detailDto = new ShipmentApplyDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.SalesOrderDetailId = detail.SalesOrderDetailId;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _shipmentApplyAppService.UpdateAsync((Guid)Model.Id, dto);
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

        private void OnSalesOrderSelected(ICollection<SalesOrderDetailSelectDto> salesOrderDetails)
        {
            foreach (var item in salesOrderDetails)
            {

                var isExsits = Model.Details.Any(m => m.SalesOrderDetailId == item.Id);
                if (isExsits)
                {
                    continue;
                }

                ShipmentApplyDetailEditModel detailEditModel = new ShipmentApplyDetailEditModel();
                detailEditModel.SalesOrderDetailId = item.Id;
                detailEditModel.Quantity = 0;
                detailEditModel.OrderQuantity = item.Quantity;
                detailEditModel.DeliveryDate = item.DeliveryDate;
                detailEditModel.ProductName = item.ProductName;
                detailEditModel.ProductNumber = item.ProductNumber;
                detailEditModel.ProductSpec = item.ProductSpec;
                detailEditModel.ProductUnitName = item.ProductUnitName;
                detailEditModel.Requirement = item.Requirement;
                detailEditModel.SalesOrderNumber = item.SalesOrderNumber;
                this.Model.Details.Add(detailEditModel);
            }
        }


    }
}

