using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.QualityManagement.FinalInspections;
using Lanpuda.ERP.QualityManagement.ProcessInspections.Dtos;
using Lanpuda.ERP.QualityManagement.ProcessInspections.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.ERP.QualityManagement.ProcessInspections
{
    public class ProcessInspectionPagedViewModel : PagedViewModelBase<ProcessInspectionDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IProcessInspectionAppService _processInspectionAppService;


        #region 搜索

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string WorkOrderNumber
        {
            get { return GetProperty(() => WorkOrderNumber); }
            set { SetProperty(() => WorkOrderNumber, value); }
        }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }
        #endregion


        public ProcessInspectionPagedViewModel(
                IServiceProvider serviceProvider,
                IProcessInspectionAppService processInspectionAppService
                )
        {
            _serviceProvider = serviceProvider;
            _processInspectionAppService = processInspectionAppService;
            this.PageTitle = "过程检验";
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                await this.QueryAsync();
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

        [Command]
        public void Create()
        {
            //ModuleManager.DefaultManager.InjectOrNavigate(RegionNames.MainContentRegion, ERPUIModuleKeys.ProcessInspection_Edit);
            if (this.WindowService != null)
            {
                ProcessInspectionEditViewModel? viewModel = _serviceProvider.GetService<ProcessInspectionEditViewModel>();
                if (viewModel != null)
                {
                    WindowService.Title = "过程检验-新建";
                    WindowService.Show("ProcessInspectionEditView", viewModel);
                }
            }
        }

        [Command]
        public void Update()
        {
            if (this.WindowService != null)
            {
                ProcessInspectionEditViewModel? viewModel = _serviceProvider.GetService<ProcessInspectionEditViewModel>();
                if (viewModel != null && SelectedModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.OnCloseCallbackAsync = this.QueryAsync;
                    WindowService.Title = "过程检验-编辑";
                    WindowService.Show("ProcessInspectionEditView", viewModel);
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
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            try
            {
                if (this.SelectedModel == null) return;
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
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
            if (this.SelectedModel == null)
            {
                return false;
            }
            return true;
        }

        [AsyncCommand]
        public async Task ConfirmeAsync()
        {
            try
            {
                if (this.SelectedModel == null) return;
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确认后无法修改", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _processInspectionAppService.ConfirmeAsync(this.SelectedModel.Id);
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

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                ProcessInspectionPagedRequestDto requestDto = new ProcessInspectionPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.WorkOrderNumber = this.WorkOrderNumber;
                requestDto.ProductName = this.ProductName;
                var result = await _processInspectionAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
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
