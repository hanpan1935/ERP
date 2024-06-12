using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products.Selects.SelectAll;
using Lanpuda.ERP.ProductionManagement.Boms;
using Lanpuda.ERP.ProductionManagement.Boms.Dtos;
using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
using Lanpuda.ERP.ProductionManagement.Mpses.Selects;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.ProductionManagement.Workshops;
using Lanpuda.ERP.ProductionManagement.Workshops.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Edits
{
    public class WorkOrderEditViewModel : EditViewModelBase<WorkOrderEditModel>
    {

        private readonly IWorkOrderAppService _workOrderAppService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkshopAppService _workshopAppService;

        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }
        public WorkOrderEditViewModel(
            IWorkOrderAppService workOrderAppService,
            IServiceProvider serviceProvider,
            IWorkshopAppService workshopAppService
            )
        {
            _workOrderAppService = workOrderAppService;
            _serviceProvider = serviceProvider;
            _workshopAppService = workshopAppService;

        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var workshops = await _workshopAppService.GetAllAsync();
                Model.WorkshopSource.Clear();
                foreach (var item in workshops)
                {
                    this.Model.WorkshopSource.Add(item);
                }

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "生产工单-编辑";
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _workOrderAppService.GetAsync(id);
                    Model.Number = result.Number;
                    if (result.WorkshopId != null)
                    {
                        Model.SelectedWorkshop = Model.WorkshopSource.Where(m=>m.Id == result.WorkshopId).FirstOrDefault();
                    }
                
                    Model.MpsId = result.MpsId;
                    Model.MpsNumber = result.MpsNumber;
                    Model.ProductId = result.ProductId;
                    Model.ProductNumber = result.ProductNumber;
                    Model.ProductName = result.ProductName;
                    Model.Quantity = result.Quantity;
                    Model.StartDate = result.StartDate;
                    Model.CompletionDate = result.CompletionDate;
                    Model.Remark = result.Remark;
                }
                else
                {
                    this.PageTitle = "生产工单-新建";
                    this.Model.Number = "系统自动生成";
                    Model.StartDate = DateTime.Now;
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
            if (CurrentWindowService != null)
            {
                CurrentWindowService.Close();
                if (OnCloseWindowCallbackAsync != null)
                {
                    await OnCloseWindowCallbackAsync();
                }
            }
        }

        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            if (hasError == true) return !hasError;
            return true;
        }


        [Command]
        public void SelectMps()
        {
            MpsSelectViewModel mpsSelectViewModel = _serviceProvider.GetRequiredService<MpsSelectViewModel>();
            mpsSelectViewModel.OnSelectedCallback = OnMpsSelectedCallback;
            this.WindowService.Title = "选择生产计划";
            this.WindowService.Show("MpsSelectView", mpsSelectViewModel);
        }

        [Command]
        public void SelectProduct()
        {
            if (this.Model.MpsId == Guid.Empty)
            {
                return;
            }

            ProductSelectAllViewModel viewModel = _serviceProvider.GetRequiredService<ProductSelectAllViewModel>();
            viewModel.OnSelectedCallback = OnProductSelectedCallback;
            this.WindowService.Title = "选择产品";
            this.WindowService.Show("ProductSelectAllView", viewModel);
        }

        public bool CanSelectProduct()
        {
            if (this.Model.MpsId == Guid.Empty)
            {
                return false;
            }
            return true;
        }
      

        private async Task UpdateAsync()
        {
            try
            {
                if (Model.Id ==null)
                {
                    return;
                }
                this.IsLoading = true;
                WorkOrderUpdateDto dto = new WorkOrderUpdateDto();
                dto.MpsId = Model.MpsId;
                dto.ProductId = Model.ProductId;
                dto.Quantity = Model.Quantity;
                dto.StartDate = Model.StartDate;
                dto.CompletionDate = Model.CompletionDate;
                dto.Remark = Model.Remark;
                if (Model.SelectedWorkshop != null)
                {
                    dto.WorkshopId = Model.SelectedWorkshop.Id;
                }
                await _workOrderAppService.UpdateAsync((Guid)Model.Id, dto);
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


    

        private void OnMpsSelectedCallback(MpsDto mps)
        {
            this.Model.MpsNumber = mps.Number;
            this.Model.MpsId = mps.Id;
        }

        private void OnProductSelectedCallback(ProductDto product)
        {
            this.Model.ProductId = product.Id;
            this.Model.ProductNumber = product.Number;
            this.Model.ProductName = product.Name;
            this.Model.ProductSpec = product.Spec;
            this.Model.ProductUnitName = product.ProductUnitName;
        }
    }
}
