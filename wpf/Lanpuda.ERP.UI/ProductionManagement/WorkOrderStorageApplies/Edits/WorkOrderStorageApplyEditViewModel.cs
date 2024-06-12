using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Selects;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Edits
{
    public class WorkOrderStorageApplyEditViewModel : EditViewModelBase<WorkOrderStorageApplyEditModel>
    {
        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }
        private readonly IWorkOrderStorageApplyAppService _workOrderStorageApplyAppService;
        private readonly IServiceProvider _serviceProvider;

        private List<ProductDto> ProductSourceList { get; set; }

        public WorkOrderStorageApplyEditViewModel(
            IWorkOrderStorageApplyAppService workOrderStorageApplyAppService,
            IServiceProvider serviceProvider
            )
        {
            _workOrderStorageApplyAppService = workOrderStorageApplyAppService;
            _serviceProvider = serviceProvider;
            Model = new WorkOrderStorageApplyEditModel();
            ProductSourceList = new List<ProductDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "入库申请-编辑";
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _workOrderStorageApplyAppService.GetAsync(id);
                    this.Model.Id = result.Id;
                    this.Model.Number = result.Number;
                    this.Model.WorkOrderNumber= result.WorkOrderNumber;
                    this.Model.WorkOrderId = result.WorkOrderId;
                    this.Model.Quantity = result.Quantity;
                    this.Model.Remark = result.Remark;
                }
                else
                {
                    this.PageTitle = "入库申请-新建";
                    this.Model.Number = "系统自动生成";
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
        public void ShowSelectWorkOrderWindow()
        {
            var viewModel = _serviceProvider.GetRequiredService<WorkOrderSelectViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = this.OnWorkOrderSelectedCallback;
                viewModel.IsConfirmed = true;
                this.WindowService.Title = "选择生产工单";
                this.WindowService.Show("WorkOrderSelectView", viewModel);
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

            if (this.OnCloseWindowCallbackAsync != null)
            {
                await OnCloseWindowCallbackAsync();
                this.CurrentWindowService.Close();
            }
        }

        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            if (hasError == true) return !hasError;
            return true;
        }

       
    

        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                WorkOrderStorageApplyCreateDto dto = new WorkOrderStorageApplyCreateDto();
                dto.Remark = Model.Remark;
                dto.WorkOrderId = Model.WorkOrderId;
                dto.Quantity = Model.Quantity;
                await _workOrderStorageApplyAppService.CreateAsync(dto);
                
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
                WorkOrderStorageApplyUpdateDto dto = new WorkOrderStorageApplyUpdateDto();
                dto.Remark = Model.Remark;
                dto.WorkOrderId = Model.WorkOrderId;
                dto.Quantity = Model.Quantity;
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _workOrderStorageApplyAppService.UpdateAsync((Guid)Model.Id, dto);
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

        private void OnWorkOrderSelectedCallback(WorkOrderDto workOrder)
        {
            this.Model.WorkOrderId = workOrder.Id;
            this.Model.WorkOrderNumber = workOrder.Number;
            this.Model.Quantity = workOrder.Quantity;
        }

    }
}
