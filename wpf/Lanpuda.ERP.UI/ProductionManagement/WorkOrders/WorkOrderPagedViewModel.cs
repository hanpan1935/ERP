using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Edits;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.ERP.ProductionManagement.Mpses.Edits;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.ERP.ProductionManagement.WorkOrders.MultipleCreates;
using Volo.Abp.ObjectMapping;
using Lanpuda.ERP.UI.ProductionManagement.WorkOrders.MultipleAutoCreates;
using System.Windows.Controls;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders
{
    public class WorkOrderPagedViewModel : PagedViewModelBase<WorkOrderPagedModel>
    {
        protected IWindowService EidtWindowService { get { return this.GetService<IWindowService>("Edit"); } }

        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkOrderAppService _workOrderAppService;
        private readonly IObjectMapper _objectMapper;

        public WorkOrderPagedRequestModel RequestModel { get; set; }

        public bool IsSelectedAll
        {
            get { return GetProperty(() => IsSelectedAll); }
            set
            {
                SetProperty(() => IsSelectedAll, value, () =>
                {
                    if (IsSelectedAll == true)
                    {
                        foreach (var item in PagedDatas)
                        {
                            item.IsSelected = true;
                        }
                    }
                    else
                    {
                        foreach (var item in PagedDatas)
                        {
                            item.IsSelected = false;
                        }
                    }
                }); }
        }


        public WorkOrderPagedViewModel(IServiceProvider serviceProvider, IWorkOrderAppService workOrderAppService, IObjectMapper objectMapper)
        {
            _serviceProvider = serviceProvider;
            _workOrderAppService = workOrderAppService;
            RequestModel = new WorkOrderPagedRequestModel();
            this.PageTitle = "生产工单";
            _objectMapper = objectMapper;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            if (this.WindowService != null)
            {
                MultipleCreateViewModel? viewModel = _serviceProvider.GetService<MultipleCreateViewModel>();
                if (viewModel != null)
                {
                    WindowService.Title = "生产工单-批量新建";
                    viewModel.OnCloseCallbackAsync = this.QueryAsync;
                    WindowService.Show("MultipleCreateView", viewModel);
                }
            }
        }

        [Command]
        public void AutoCreate() 
        {
            if (this.WindowService != null)
            {
                MultipleAutoCreateViewModel? viewModel = _serviceProvider.GetService<MultipleAutoCreateViewModel>();
                if (viewModel != null)
                {
                    WindowService.Title = "生产工单-批量新建";
                    viewModel.OnCloseCallbackAsync = this.QueryAsync;
                    WindowService.Show("MultipleAutoCreateView", viewModel);
                }
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null) { return; }
            if (this.EidtWindowService != null)
            {
                WorkOrderEditViewModel? workOrderEditViewModel = _serviceProvider.GetService<WorkOrderEditViewModel>();
                if (workOrderEditViewModel != null)
                {
                    EidtWindowService.Title = "生产工单-编辑";
                    workOrderEditViewModel.Model.Id = this.SelectedModel.Id;
                    workOrderEditViewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                    EidtWindowService.Show("WorkOrderEditView", workOrderEditViewModel);
                }
            }
        }

        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsConfirmed == true)
            {
                return false;
            }
            return true;
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.RequestModel.Number = string.Empty;
            this.RequestModel.MpsNumber = string.Empty;
            this.RequestModel.ProductName = string.Empty;
            this.RequestModel.IsConfirmed = null;
            this.RequestModel.StartDate = null;
            this.RequestModel.CompletionDate = null;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _workOrderAppService.DeleteAsync(this.SelectedModel.Id);
                    await this.QueryAsync();
                }
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

        public bool CanDeleteAsync()
        {
            if (this.SelectedModel == null) return false;
            return true;
        }



        [AsyncCommand]
        public async Task ConfirmeAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                this.IsLoading = true;
                await _workOrderAppService.ConfirmeAsync(this.SelectedModel.Id);
                await this.QueryAsync();
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

        public bool CanConfirmeAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsConfirmed == true)
            {
                return false;
            }
            return true;
        }


        [AsyncCommand]
        public async Task MultipleConfirmeAsync()
        {
            try
            {
                this.IsLoading = true;
                await Task.Delay(1000);
                List<Guid> ids = new List<Guid>();
                foreach (var item in this.PagedDatas)
                {
                    if (item.IsSelected == true)
                    {
                        ids.Add(item.Id);
                    }
                }

                await this._workOrderAppService.MultipleConfirmeAsync(ids);
                await this.QueryAsync();
                this.IsSelectedAll = false;
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


        [AsyncCommand]
        public async Task SortingAsync(DataGridSortingEventArgs e)
        {
            if (e.Column.SortMemberPath == nameof(WorkOrderDto.ProductName))
            {
                this.RequestModel.Sorting = nameof(WorkOrderDto.ProductId);
            }
            else if (e.Column.SortMemberPath == nameof(WorkOrderDto.WorkshopName))
            {
                this.RequestModel.Sorting = nameof(WorkOrderDto.WorkshopId);
            }
            else
            {
                this.RequestModel.Sorting = e.Column.SortMemberPath;
            }
            var aa = e.Column.SortDirection;
            await this.QueryAsync();
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                WorkOrderPagedRequestDto requestDto = new WorkOrderPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.RequestModel.Number;
                requestDto.MpsNumber = this.RequestModel.MpsNumber;
                requestDto.ProductName = this.RequestModel.ProductName;
                requestDto.IsConfirmed = this.RequestModel.IsConfirmed;
                requestDto.StartDate = this.RequestModel.StartDate;
                requestDto.CompletionDate = this.RequestModel.CompletionDate;
                requestDto.Sorting = this.RequestModel.Sorting;
                var result = await _workOrderAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    WorkOrderPagedModel model = _objectMapper.Map<WorkOrderDto, WorkOrderPagedModel>(item);
                    this.PagedDatas.Add(model);
                }
                this.PagedDatas.CanNotify = true;
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
    }
}
