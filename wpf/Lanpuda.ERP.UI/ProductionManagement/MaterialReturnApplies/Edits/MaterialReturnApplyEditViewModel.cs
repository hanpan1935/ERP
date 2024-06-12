using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.MaterialApplies.Edits;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Selects;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Selects;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Edits
{
    public class MaterialReturnApplyEditViewModel : EditViewModelBase<MaterialReturnApplyEditModel>
    {
        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }
        private readonly IMaterialReturnApplyAppService _materialReturnApplyAppService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IObjectMapper _objectMapper;
       

        public MaterialReturnApplyEditViewModel(
            IMaterialReturnApplyAppService materialReturnApplyAppService,
            IServiceProvider serviceProvider,
            IObjectMapper objectMapper
            )
        {
            _materialReturnApplyAppService = materialReturnApplyAppService;
            _serviceProvider = serviceProvider;
            _objectMapper = objectMapper;
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
                    var result = await _materialReturnApplyAppService.GetAsync(id);
                    this.Model.Id = result.Id;
                    this.Model.Number = result.Number;
                    this.Model.Remark = result.Remark;
                    this.Model.Details.Clear();
                    foreach (var detail in result.Details)
                    {
                        MaterialReturnApplyDetailEditModel detailModel = new MaterialReturnApplyDetailEditModel();
                        detailModel.Id = detail.Id;
                        detailModel.WorkOrderOutDetailId = detail.WorkOrderOutDetailId;
                        detailModel.ProductId = detail.ProductId;
                        detailModel.ProductNumber = detail.ProductNumber;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductSpec = detail.ProductSpec;
                        detailModel.ProductUnitName = detail.ProductUnitName;
                        detailModel.Batch = detail.Batch;
                        detailModel.Quantity = detail.Quantity;
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
        public void ShowSelectWorkOrderOutWindow()
        {
            if (this.WindowService != null)
            {
                WorkOrderOutDetailMultipleSelectViewModel? viewModel = _serviceProvider.GetService<WorkOrderOutDetailMultipleSelectViewModel>();
                if (viewModel != null)
                {
                    WindowService.Title = "选择已领物料";
                    viewModel.SaveCallback = this.OnWorkOrderOutSelected;
                    WindowService.Show("WorkOrderOutDetailMultipleSelectView", viewModel);
                }
            }
        }

        private void OnWorkOrderOutSelected(ICollection<WorkOrderOutDetailSelectDto> details)
        {
            foreach (var item in details)
            {
                MaterialReturnApplyDetailEditModel detailModel = new MaterialReturnApplyDetailEditModel();
                detailModel.WorkOrderOutDetailId = item.Id;
                detailModel.ProductId= item.ProductId;
                detailModel.ProductNumber = item.ProductNumber;
                detailModel.ProductName = item.ProductName;
                detailModel.ProductSpec = item.ProductSpec;
                detailModel.ProductUnitName = item.ProductUnitName;
                detailModel.Batch = item.Batch;
                detailModel.Quantity = 0;
                this.Model.Details.Add(detailModel);
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
            }
            this.Close();
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

        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                MaterialReturnApplyCreateDto dto = new MaterialReturnApplyCreateDto();
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    MaterialReturnApplyDetailCreateDto detailDto = new MaterialReturnApplyDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.WorkOrderOutDetailId =detail.WorkOrderOutDetailId;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                await _materialReturnApplyAppService.CreateAsync(dto);
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
                MaterialReturnApplyUpdateDto dto = new MaterialReturnApplyUpdateDto();
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    MaterialReturnApplyDetailUpdateDto detailDto = new MaterialReturnApplyDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.WorkOrderOutDetailId = detail.WorkOrderOutDetailId;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _materialReturnApplyAppService.UpdateAsync((Guid)Model.Id, dto);
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
