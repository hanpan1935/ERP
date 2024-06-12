using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.Inventories.Selects;
using Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts.Edits
{
    public class SalesOutEditViewModel : EditViewModelBase<SalesOutEditModel>
    {
        private readonly ISalesOutAppService _salesOutAppService;
        private readonly IServiceProvider _serviceProvider;
        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }

        public SalesOutEditViewModel(
            ISalesOutAppService salesOutAppService,
            IServiceProvider serviceProvider
            )
        {
            _salesOutAppService = salesOutAppService;
            _serviceProvider = serviceProvider;
        }
    

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                if (Model.Id != Guid.Empty)
                {
                    this.PageTitle = "发货申请-编辑";
                    var result = await _salesOutAppService.GetAsync(Model.Id);
                    Model.ShipmentApplyNumber = result.ShipmentApplyNumber;
                    Model.Number = result.Number;
                    Model.ProductId = result.ProductId;
                    Model.ProductName = result.ProductName;
                    Model.Quantity = result.ApplyQuantity;
                    Model.Remark = result.Remark;
                    Model.Details.Clear();
                    foreach (var item in result.Details)
                    {
                        SalesOutDetailEditModel detailEditModel = new SalesOutDetailEditModel(Model);
                        detailEditModel.LocationId = item.LocationId;
                        detailEditModel.Batch = item.Batch;
                        detailEditModel.Quantity = item.Quantity;
                        Model.Details.Add(detailEditModel);
                    }
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
        public async Task SaveAsync()
        {
            await UpdateAsync();
        }

        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }

        [Command]
        public void ShowSelectInventoryWindow()
        {
            var viewModel = _serviceProvider.GetService<InventorySelectByProductViewModel>();
            if (viewModel != null)
            {
                viewModel.ProductId = Model.ProductId;
                viewModel.ProductName = Model.ProductName;
                viewModel.OnSelectedCallback = this.OnInventorySelectedCallback;
                this.WindowService.Title = "选择库存-销售出库-"+ Model.ProductName;
                this.WindowService.Show("InventorySelectByProductView", viewModel);
            }
        }


        [Command]
        public void DeleteSelectedRow()
        {
            if (this.Model.SelectedRow != null)
            {
                this.Model.Details.Remove(Model.SelectedRow);
            }
        }

        public bool CanDeleteSelectedRow()
        {
            if (this.Model.SelectedRow != null)
            {
                return false;
            }
            return true;
        }


        [AsyncCommand]
        public async Task AutoOutAsync()
        {
            try
            {
                this.IsLoading = true;
                var detailList =  await _salesOutAppService.AutoOutAsync(this.Model.Id);
                if (detailList.Count >0)
                {
                    this.Model.Details.Clear();
                }
                else
                {
                    HandyControl.Controls.MessageBox.Show("没有库存可以出库", "警告!");
                }
                foreach (var item in detailList)
                {
                    SalesOutDetailEditModel detailEditModel = new SalesOutDetailEditModel(this.Model);
                    detailEditModel.LocationId = item.LocationId;
                    detailEditModel.WarehouseName = item.WarehouseName;
                    detailEditModel.LocationName = item.LocationName;
                    detailEditModel.Quantity = item.Quantity;
                    detailEditModel.Batch = item.Batch;
                    this.Model.Details.Add(detailEditModel);
                    this.Model.NotifyTotalQuantityChanged();
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



        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                SalesOutUpdateDto dto = new SalesOutUpdateDto();
                dto.Remark = Model.Remark;
                foreach (var detail in Model.Details)
                {
                    SalesOutDetailUpdateDto detailDto = new SalesOutDetailUpdateDto();
                    detailDto.Id = detail.Id;
                    detailDto.LocationId = detail.LocationId;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.Batch = detail.Batch;
                    dto.Details.Add(detailDto);
                }

                await _salesOutAppService.UpdateAsync((Guid)Model.Id, dto);
                if (this.OnCloseWindowCallbackAsync != null)
                {
                    await OnCloseWindowCallbackAsync();
                }
                this.Close();
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


        private void OnInventorySelectedCallback(InventoryDto inventory)
        {
            var hasSame = Model.Details
                .Any( m=>m.LocationId== inventory.LocationId &&m.Batch == inventory.Batch);
            if (hasSame)
            {
                Growl.Info("已经添加了");
                return;
            }

            SalesOutDetailEditModel detailEditModel = new SalesOutDetailEditModel(Model);
            detailEditModel.LocationId = inventory.LocationId;
            detailEditModel.WarehouseName= inventory.WarehouseName;
            detailEditModel.LocationName = inventory.LocationName;
            detailEditModel.Batch = inventory.Batch;
            Model.Details.Add(detailEditModel);
        }
    }
}

