using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelect;
using Lanpuda.ERP.ProductionManagement.Mpses;
using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
using Lanpuda.ERP.ProductionManagement.Mpses.Selects;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.ProductionManagement.Workshops;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Timing;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.MultipleCreates
{
    public class MultipleCreateViewModel : EditViewModelBase<MultipleCreateModel>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkOrderAppService _workOrderAppService;
        private readonly IWorkshopAppService _workshopAppService;
        private readonly IMpsAppService _mpsAppService;
        public Func<Task>? OnCloseCallbackAsync { get; set; }

        public MultipleCreateViewModel(
            IServiceProvider serviceProvider, 
            IWorkOrderAppService workOrderAppService,
            IMpsAppService mpsAppService,
            IWorkshopAppService workshopAppService)
        {
            _serviceProvider = serviceProvider;
            _workOrderAppService = workOrderAppService;
            _workshopAppService = workshopAppService;
            _mpsAppService = mpsAppService;
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
        public void SelectMps()
        {
            MpsSelectViewModel mpsSelectViewModel = _serviceProvider.GetRequiredService<MpsSelectViewModel>();
            mpsSelectViewModel.OnSelectedCallback = OnMpsSelectedCallback;
            this.WindowService.Title = "选择生产计划";
            this.WindowService.Show("MpsSelectView", mpsSelectViewModel);
        }

        private void OnMpsSelectedCallback(MpsDto mps)
        {
            this.Model.MpsNumber = mps.Number;
            this.Model.MpsId = mps.Id;
        }


        [Command]
        public void SelectProduct()
        {
            ProductMultipleSelectViewModel viewModel = _serviceProvider.GetRequiredService<ProductMultipleSelectViewModel>();
            viewModel.SaveCallback = OnProductSelectedCallback;
            this.WindowService.Title = "选择产品";
            this.WindowService.Show("ProductMultipleSelectView", viewModel);
        }


        private void OnProductSelectedCallback(ICollection<ProductDto> products)
        {
            foreach (var item in products)
            {
                MultipleCreateDetailModel detailModel = new MultipleCreateDetailModel(this.Model);
                detailModel.ProductId = item.Id;
                detailModel.Quantity = 0;
                detailModel.StartDate = DateTime.Now.ToLocalTime();
                detailModel.Remark = null;
                detailModel.ProductNumber = item.Number;
                detailModel.ProductName = item.Name;
                detailModel.ProductSpec = item.Spec;
                detailModel.ProductUnitName = item.ProductUnitName;
                detailModel.LeadTime = item.LeadTime;
                if (item.LeadTime != null)
                {
                    int leadTime = (int)item.LeadTime;
                    detailModel.CompletionDate = detailModel.StartDate.AddDays(leadTime);
                }

                if (item.DefaultWorkshopId != null)
                {
                    detailModel.SelectedWorkshop = Model.WorkshopSource.Where(m => m.Id == item.DefaultWorkshopId).FirstOrDefault();
                }

                this.Model.Details.Add(detailModel);
            }
        }


        [AsyncCommand]
        public async Task SaveAsync()
        {
            try
            {
                this.IsLoading = true;
                WorkOrderMultipleCreateDto dto = new WorkOrderMultipleCreateDto();
                dto.MpsId = Model.MpsId;
                foreach (var item in Model.Details)
                {
                    WorkOrderMultipleCreateDetailDto detailDto = new WorkOrderMultipleCreateDetailDto();
                    if (item.SelectedWorkshop != null)
                    {
                        detailDto.WorkshopId = item.SelectedWorkshop.Id;
                    }
                    detailDto.ProductId = item.ProductId;
                    detailDto.Quantity = item.Quantity;
                    detailDto.StartDate = item.StartDate;
                    detailDto.CompletionDate = item.CompletionDate;
                    detailDto.Remark = item.Remark;
                    dto.Details.Add(detailDto);
                }
                await _workOrderAppService.MultipleCreateAsync(dto);

                if (OnCloseCallbackAsync != null)
                {
                    await OnCloseCallbackAsync();
                }
                this.CurrentWindowService.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        public bool CanSaveAsync()
        {
            if (this.Model.HasErrors())
            {
                return false;
            }
            
            return true;
        }

        [Command]
        public void CopyRow()
        {
            if (this.Model.SelectedRow == null)
            {
                return;
            }
            MultipleCreateDetailModel detailModel = new MultipleCreateDetailModel(this.Model);
            detailModel.SelectedWorkshop = Model.SelectedRow.SelectedWorkshop;
            detailModel.ProductId = Model.SelectedRow.ProductId;
            detailModel.Quantity = Model.SelectedRow.Quantity;
            detailModel.StartDate = Model.SelectedRow.StartDate;
            detailModel.CompletionDate = Model.SelectedRow.CompletionDate;
            detailModel.Remark = Model.SelectedRow.Remark;
            detailModel.ProductNumber = Model.SelectedRow.ProductNumber;
            detailModel.ProductName = Model.SelectedRow.ProductName;
            detailModel.ProductSpec = Model.SelectedRow.ProductSpec;
            detailModel.ProductUnitName = Model.SelectedRow.ProductUnitName;
            detailModel.LeadTime = Model.SelectedRow.LeadTime;
            Model.Details.InsertAfter(this.Model.SelectedRow, detailModel);
        }

        [Command]
        public void DeleteSelectedRow()
        {
            if (this.Model.SelectedRow == null)
            {
                return;
            }
            this.Model.Details.Remove(this.Model.SelectedRow);
        }


      
    }
}
