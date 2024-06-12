//using DevExpress.Mvvm.DataAnnotations;
//using DevExpress.Mvvm;
//using Lanpuda.Client.Mvvm;
//using Lanpuda.ERP.BasicData.Products.Dtos;
//using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
//using Lanpuda.ERP.ProductionManagement.WorkOrders.Selects;
//using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Dtos;
//using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Edits;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.DependencyInjection;

//namespace Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.MultipleCreates
//{
//    public class WorkOrderStorageApplyMultipleCreateViewModel : RegionViewModelBase
//    {
//        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
//        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }

//        private readonly IWorkOrderStorageApplyAppService _workOrderStorageApplyAppService;

//        private readonly IServiceProvider _serviceProvider;

//        public WorkOrderStorageApplyEditModel Model { get; set; }


//        private List<ProductDto> ProductSourceList { get; set; }

//        public WorkOrderStorageApplyMultipleCreateViewModel(
//            IWorkOrderStorageApplyAppService workOrderStorageApplyAppService,
//            IServiceProvider serviceProvider,
//            )
//        {
//            _workOrderStorageApplyAppService = workOrderStorageApplyAppService;
//            _serviceProvider = serviceProvider;
//            Model = new WorkOrderStorageApplyEditModel();
//            ProductSourceList = new List<ProductDto>();
//        }


//        [AsyncCommand]
//        public async Task InitializeAsync()
//        {
//            try
//            {
//                this.IsLoading = true;

//                if (Model.Id != null && Model.Id != Guid.Empty)
//                {
//                    this.PageTitle = "入库申请-编辑";
//                    if (Model.Id == null) throw new Exception("Id为空");
//                    Guid id = (Guid)Model.Id;
//                    var result = await _workOrderStorageApplyAppService.GetAsync(id);
//                    this.Model.Id = result.Id;
//                    this.Model.Number = result.Number;
//                    this.Model.Remark = result.Remark;
//                    this.Model.Details.Clear();
                   
//                }
//                else
//                {
//                    this.PageTitle = "入库申请-新建";
//                }
//            }
//            catch (Exception e)
//            {
//                HandleException(e);
//            }
//            finally
//            {
//                this.IsLoading = false;
//            }
//        }


//        [Command]
//        public void DeleteSelectedRow()
//        {
//            if (Model.SelectedRow != null)
//            {
//                this.Model.Details.Remove(Model.SelectedRow);
//            }
//        }

//        [Command]
//        public void ShowSelectWorkOrderWindow()
//        {
//            var viewModel = _serviceProvider.GetRequiredService<WorkOrderMultipleSelectViewModel>();
//            if (viewModel != null)
//            {
//                viewModel.OnSelectedCallback = this.OnWorkOrderSelectedCallback;
//                this.WindowService.Title = "选择生产工单";
//                this.WindowService.Show("WorkOrderMultipleSelectView", viewModel);
//            }
//        }


//        [AsyncCommand]
//        public async Task SaveAsync()
//        {
//            if (Model.Id == null || Model.Id == Guid.Empty)
//            {
//                await CreateAsync();
//            }
//            else
//            {
//                await UpdateAsync();
//            }

//            if (this.OnCloseWindowCallbackAsync != null)
//            {
//                await OnCloseWindowCallbackAsync();
//                this.CurrentWindowService.Close();
//            }
//        }

//        public bool CanSaveAsync()
//        {
//            bool hasError = Model.HasErrors();
//            if (hasError == true) return !hasError;
//            if (Model.Details.Count == 0)
//            {
//                return false;
//            }
//            return true;
//        }

//        [Command]
//        public void Close()
//        {
//            if (CurrentWindowService != null)
//                CurrentWindowService.Close();
//        }


//        private async Task CreateAsync()
//        {
//            try
//            {
//                this.IsLoading = true;
//                WorkOrderStorageApplyCreateDto dto = new WorkOrderStorageApplyCreateDto();
//                dto.Remark = Model.Remark;

              
//                await _workOrderStorageApplyAppService.CreateAsync(dto);
//                if (CurrentWindowService != null)
//                    CurrentWindowService.Close();
//            }
//            catch (Exception e)
//            {
//                HandleException(e);
//                throw;
//            }
//            finally
//            {
//                this.IsLoading = false;
//            }
//        }

//        private async Task UpdateAsync()
//        {
//            try
//            {
//                this.IsLoading = true;
//                WorkOrderStorageApplyUpdateDto dto = new WorkOrderStorageApplyUpdateDto();
//                dto.Remark = Model.Remark;

                
//                if (Model.Id == null)
//                {
//                    throw new ArgumentNullException("", "Id不能为空");
//                }
//                await _workOrderStorageApplyAppService.UpdateAsync((Guid)Model.Id, dto);
//                if (CurrentWindowService != null)
//                    CurrentWindowService.Close();
//            }
//            catch (Exception e)
//            {
//                HandleException(e);
//                throw;
//            }
//            finally
//            {
//                this.IsLoading = false;
//            }
//        }

//        private void OnWorkOrderSelectedCallback(ICollection<WorkOrderDto> workOrders)
//        {
//            foreach (var item in workOrders)
//            {
//                WorkOrderStorageApplyDetailEditModel detailEditModel = new WorkOrderStorageApplyDetailEditModel();
//                detailEditModel.WorkOrderId = item.Id;
//                detailEditModel.WorkOrderNumber = item.Number;
//                detailEditModel.WorkOrderQuantity = item.Quantity;
//                detailEditModel.ProductName = item.ProductName;
//                detailEditModel.ProductSpec = item.ProductSpec;
//                detailEditModel.ProductUnitName = item.ProductUnitName;
//                detailEditModel.Quantity = item.Quantity;
//                this.Model.Details.Add(detailEditModel);
//            }
//        }

//    }
//}
