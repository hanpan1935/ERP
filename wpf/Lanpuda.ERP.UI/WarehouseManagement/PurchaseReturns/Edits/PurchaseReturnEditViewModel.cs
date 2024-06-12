using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.Inventories.Selects;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Edits
{
    public class PurchaseReturnEditViewModel : EditViewModelBase<PurchaseReturnEditModel>
    {

        private readonly IPurchaseReturnAppService _purchaseReturnAppService;
        private readonly IServiceProvider _serviceProvider;
        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }

        public PurchaseReturnEditViewModel(
            IPurchaseReturnAppService purchaseReturnAppService,
            IServiceProvider serviceProvider
            )
        {
            _purchaseReturnAppService = purchaseReturnAppService;
            _serviceProvider = serviceProvider;
            Model = new PurchaseReturnEditModel();
        }

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                Guid id = (Guid)Model.Id;
                var result = await _purchaseReturnAppService.GetAsync(id);
                Model.Number = result.Number;
                Model.PurchaseReturnApplyNumber = result.PurchaseReturnApplyNumber;
                Model.Remark = result.Remark;
                Model.ProductId = result.ProductId;
                Model.ProductName = result.ProductName;
                Model.ProductSpec = result.ProductSpec;
                Model.ProductUnitName = result.ProductUnitName;
                Model.ApplyQuantity = result.ApplyQuantity;
                Model.Batch = result.Batch;

                foreach (var item in result.Details)
                {
                    PurchaseReturnDetailEditModel editModel = new PurchaseReturnDetailEditModel(Model);
                    editModel.Id = item.Id;
                    editModel.WarehouseName = item.WarehouseName;
                    editModel.LocationName = item.LocationName;
                    editModel.Batch = item.Batch;
                    editModel.Quantity = item.Quantity;
                    Model.Details.Add(editModel);
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
            if (hasError == true) return !hasError;

            foreach (var item in Model.Details)
            {
                if (item.HasErrors())
                {
                    return false;
                }
            }
            return true;
        }


        [Command]
        public void ShowSelectInventoryWindow(Guid purchaseStorageDetailId)
        {
            InventorySelectByProductViewModel? selectViewModel = _serviceProvider.GetService<InventorySelectByProductViewModel>();
            if (selectViewModel != null)
            {
                WindowService.Title = "请选择库存" + Model.ProductName;
                selectViewModel.OnSelectedCallback = this.OnSelectedCallback;
                selectViewModel.ProductId = Model.ProductId;
                selectViewModel.ProductName = Model.ProductName;
                selectViewModel.Batch = Model.Batch;
                WindowService.Show("InventorySelectByProductView", selectViewModel);
            }
        }


        private void OnSelectedCallback(InventoryDto inventory)
        {
            //判断批次号是否正确
            if (inventory.Batch != Model.Batch)
            {
                Growl.Info("批次选择错误");
                return;
            }


            if (inventory.ProductId != Model.ProductId)
            {
                Growl.Info("产品选择错误");
                return;
            }

            //判断不要重复选择
            var any = this.Model.Details.Any(m=>m.Batch == inventory.Batch && m.LocationId == inventory.Id);
            if (any)
            {
                Growl.Info("已经存在相同的库存");
                return;
            }
            PurchaseReturnDetailEditModel editModel = new PurchaseReturnDetailEditModel(Model);
            editModel.WarehouseName = inventory.WarehouseName;
            editModel.LocationName = inventory.LocationName;
            editModel.LocationId = inventory.LocationId;
            editModel.Batch = inventory.Batch;
            editModel.Quantity = 0;
            Model.Details.Add(editModel);
        }


        [Command]
        public void DeleteSelectedRow()
        {
            if (Model.SelectedRow != null)
            {
                Model.Details.Remove(Model.SelectedRow);
            }
        }


        [Command]
        public async Task AutoOutAsync()
        {
            try
            {
                this.IsLoading = true;
                var detailList = await _purchaseReturnAppService.AutoOutAsync(this.Model.Id);
                if (detailList != null && detailList.Count >0)
                {
                    this.Model.Details.Clear();
                    foreach (var item in detailList)
                    {
                        PurchaseReturnDetailEditModel editModel = new PurchaseReturnDetailEditModel(Model);
                        editModel.WarehouseName = item.WarehouseName;
                        editModel.LocationName = item.LocationName;
                        editModel.LocationId = item.LocationId;
                        editModel.Batch = item.Batch;
                        if (detailList.Count == 1)
                        {
                            editModel.Quantity = Model.ApplyQuantity;
                        }
                        else
                        {
                            editModel.Quantity = 0;
                        }
                       
                        Model.Details.Add(editModel);
                        Model.NotifyTotalQuantityChanged();
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


        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                PurchaseReturnUpdateDto dto = new PurchaseReturnUpdateDto();
                dto.Remark = Model.Remark;
                for (int j = 0; j < Model.Details.Count; j++)
                {
                    var detail = Model.Details[j];
                    PurchaseReturnDetailUpdateDto detailDto = new PurchaseReturnDetailUpdateDto();
                    detailDto.Id = detail.Id;
                    detailDto.LocationId = detail.LocationId;
                    detailDto.Batch = detail.Batch;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                await _purchaseReturnAppService.UpdateAsync(Model.Id, dto);
                if (OnCloseWindowCallbackAsync != null)
                {
                    await OnCloseWindowCallbackAsync();
                }
                CurrentWindowService.Close();

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
