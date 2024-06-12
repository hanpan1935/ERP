using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.Inventories.Selects;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Edits
{
    public class WorkOrderOutEditViewModel : EditViewModelBase<WorkOrderOutEditModel>
    {
        private readonly IWorkOrderOutAppService _workOrderOutAppService;
        private readonly IServiceProvider _serviceProvider;

        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }

        public WorkOrderOutEditViewModel(
            IWorkOrderOutAppService workOrderOutAppService,
            IServiceProvider serviceProvider
            )
        {
            _workOrderOutAppService = workOrderOutAppService;
            _serviceProvider = serviceProvider;
        }
  

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                this.PageTitle = "生产领料-编辑";
                Guid id = (Guid)Model.Id;
                WorkOrderOutDto result = await _workOrderOutAppService.GetAsync(id);
                Model.Id = id;
                Model.Number = result.Number;
                Model.MaterialApplyNumber = result.MaterialApplyNumber;
                Model.Remark = result.Remark;
                Model.ProductId = result.ProductId;
                Model.ProductName = result.ProductName;
                Model.ProductSpec = result.ProductSpec;
                Model.ProductUnitName = result.ProductUnitName;
                Model.ApplyQuantity = result.ApplyQuantity;
                foreach (var item in result.Details)
                {
                    WorkOrderOutDetailEditModel detailModel = new WorkOrderOutDetailEditModel(Model);
                    detailModel.Id = item.Id;
                    detailModel.LocationId = item.LocationId;
                    detailModel.WarehouseName = item.WarehouseName;
                    detailModel.LocationName = item.LocationName;
                    detailModel.Batch = item.Batch;
                    detailModel.Quantity = item.Quantity;
                    Model.Details.Add(detailModel);
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
            if (this.OnCloseWindowCallbackAsync != null)
            {
                await OnCloseWindowCallbackAsync();
            }
            this.Close();
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
        public void ShowSelectInventoryView()
        {
            InventorySelectByProductViewModel selectViewModel = _serviceProvider.GetRequiredService<InventorySelectByProductViewModel>();
            selectViewModel.ProductId = Model.ProductId;
            selectViewModel.ProductName = Model.ProductName;
            selectViewModel.OnSelectedCallback = OnSelectedCallback;
            this.WindowService.Title = "库存选择 - " + Model.ProductName;
            this.WindowService.Show("InventorySelectByProductView", selectViewModel);
        }


        private void OnSelectedCallback(InventoryDto inventory)
        {

            if (inventory.ProductId != Model.ProductId)
            {
                Growl.Info("请选择正确的产品");
                return;
            }

            var any = Model.Details.Any(m => m.LocationId == inventory.LocationId && m.Batch == inventory.Batch);
            if (any)
            {
                Growl.Info("已经存在相同的库存");
                return;
            }

            WorkOrderOutDetailEditModel detailEditModel = new WorkOrderOutDetailEditModel(Model);
            detailEditModel.LocationId = inventory.LocationId;
            detailEditModel.Batch = inventory.Batch;
            detailEditModel.WarehouseName = inventory.WarehouseName;
            detailEditModel.LocationName = inventory.LocationName;
            this.Model.Details.Add(detailEditModel);
        }

        [Command]
        public void DeleteSelectedRow()
        {
            if (this.Model.SelectedRow != null)
            {
                this.Model.Details.Remove(this.Model.SelectedRow);
                Model.NotifyTotalQuantityChanged();
            }
        }


        [AsyncCommand]
        public async Task AutoOutAsync()
        {
            try
            {
                this.IsLoading = true;
                var list = await _workOrderOutAppService.AutoOutAsync(this.Model.Id);
                if (list != null && list.Count >0)
                {
                    this.Model.Details.Clear();
                    foreach (var item in list)
                    {
                        WorkOrderOutDetailEditModel editModel = new WorkOrderOutDetailEditModel(this.Model);
                        editModel.WarehouseName = item.WarehouseName;
                        editModel.LocationName = item.LocationName;
                        editModel.LocationId = item.LocationId;
                        editModel.Batch = item.Batch;
                        editModel.Quantity = item.Quantity;
                        this.Model.Details.Add(editModel);
                        this.Model.NotifyTotalQuantityChanged();
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
                WorkOrderOutUpdateDto dto = new WorkOrderOutUpdateDto();
                dto.Remark = this.Model.Remark;
                for (int j = 0; j < Model.Details.Count; j++)
                {
                    var detail = Model.Details[j];
                    WorkOrderOutDetailUpdateDto detailDto = new WorkOrderOutDetailUpdateDto();
                    detailDto.Id = detail.Id;
                    detailDto.LocationId = detail.LocationId;
                    detailDto.Batch = detail.Batch;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                await _workOrderOutAppService.UpdateAsync(Model.Id, dto);


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
