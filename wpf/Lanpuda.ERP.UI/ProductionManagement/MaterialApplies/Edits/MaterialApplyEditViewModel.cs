using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.Client.Common;
using Lanpuda.ERP.ProductionManagement.Boms;
using Lanpuda.ERP.ProductionManagement.Boms.Dtos;
using DevExpress.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.ObjectMapping;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Selects;
using Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using System.Windows;
using DevExpress.Mvvm.UI;
using Lanpuda.ERP.BasicData.Products.Selects.SelectAll;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies.Edits
{
    public class MaterialApplyEditViewModel : EditViewModelBase<MaterialApplyEditModel>
    {
        private readonly IMaterialApplyAppService _materialApplyAppService;
        private readonly IServiceProvider _serviceProvider;
        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }
        public MaterialApplyEditViewModel(
            IMaterialApplyAppService materialApplyAppService,
            IServiceProvider serviceProvider
            )
        {
            _materialApplyAppService = materialApplyAppService;
            _serviceProvider = serviceProvider;
            Model = new MaterialApplyEditModel();
        }
     

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "领料申请-编辑";
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _materialApplyAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.WorkOrderId = result.WorkOrderId;
                    Model.WorkOrderNumber = result.WorkOrderNumber;
                    Model.Remark = result.Remark;

                    this.Model.Details.Clear();
                    foreach (var detail in result.Details)
                    {
                        MaterialApplyDetailEditModel detailModel = new MaterialApplyDetailEditModel();
                        detailModel.Id = detail.Id;
                        detailModel.ProductId = detail.ProductId;
                        detailModel.ProductNumber = detail.ProductNumber;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductUnitName = detail.ProductUnitName;
                        detailModel.ProductSpec = detail.ProductSpec;
                        detailModel.Quantity = detail.Quantity;
                        detailModel.StandardQuantity = detail.StandardQuantity;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "领料申请-新建";
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
        public void ShowSelectProductWindow()
        {
            ProductSelectAllViewModel viewModel = _serviceProvider.GetRequiredService<ProductSelectAllViewModel>();
            viewModel.OnSelectedCallback = this.OnProductSelectedCallback;
            WindowService.Title = "选择产品";
            this.WindowService.Show("ProductSelectAllView", viewModel);
        }

        [Command]
        public void DeleteSelectedRow()
        {
            if (Model.SelectedRow != null)
            {
                this.Model.Details.Remove(Model.SelectedRow);
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
                this.Close();
            }
        }
        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            if (hasError == true) return !hasError;
            if (Model.Details.Count == 0)
            {
                return false;
            }
            return true;
        }


        [Command]
        public void ShowSelectWorkOrderWindow()
        {
            WorkOrderSelectViewModel viewModel = _serviceProvider.GetRequiredService<WorkOrderSelectViewModel>();
            viewModel.OnSelectedCallback = this.OnWorkOrderSelectedCallback;
            viewModel.IsConfirmed = true;
            WindowService.Title = "选择生产工单";
            this.WindowService.Show("WorkOrderSelectView", viewModel);
        }

        [Command]
        public void InputQuantityByStandard()
        {
            foreach (var item in Model.Details)
            {
                item.Quantity = item.StandardQuantity;
            }
        }


        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                MaterialApplyCreateDto dto = new MaterialApplyCreateDto();
                dto.Remark = Model.Remark;
                dto.WorkOrderId= Model.WorkOrderId;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    MaterialApplyDetailCreateDto detailDto = new MaterialApplyDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.StandardQuantity = detail.StandardQuantity;
                    detailDto.Remark = detail.Remark;
                    dto.Details.Add(detailDto);
                }
                await _materialApplyAppService.CreateAsync(dto);
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
                MaterialApplyUpdateDto dto = new MaterialApplyUpdateDto();
                dto.WorkOrderId = Model.WorkOrderId;
                dto.Remark = Model.Remark;

                for (int i = 0; i < Model.Details.Count; i++)
                {
                    MaterialApplyDetailUpdateDto detailDto = new MaterialApplyDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.StandardQuantity = detail.StandardQuantity;
                    detailDto.Remark = detail.Remark;
                    detailDto.Id = detail.Id;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _materialApplyAppService.UpdateAsync((Guid)Model.Id, dto);
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
            this.Model.Details.Clear();
            foreach (var item in workOrder.StandardMaterialDetails)
            {
                MaterialApplyDetailEditModel detailModel = new MaterialApplyDetailEditModel();
                detailModel.ProductId = item.ProductId;
                detailModel.ProductNumber = item.ProductNumber;
                detailModel.ProductName = item.ProductName;
                detailModel.ProductSpec = item.ProductSpec;
                detailModel.ProductUnitName = item.ProductUnitName;
                detailModel.StandardQuantity = item.Quantity;
                Model.Details.Add(detailModel);
            }
        }

        private void OnProductSelectedCallback(ProductDto product)
        {
            MaterialApplyDetailEditModel detailModel= new MaterialApplyDetailEditModel();
            detailModel.ProductId= product.Id;
            detailModel.ProductName = product.Name;
            detailModel.ProductNumber = product.Number;
            detailModel.ProductSpec = product.Spec;
            detailModel.ProductUnitName = product.ProductUnitName;
            detailModel.Quantity = 0;
            detailModel.StandardQuantity = 0;

            this.Model.Details.Add(detailModel) ;
        }
    }
}
