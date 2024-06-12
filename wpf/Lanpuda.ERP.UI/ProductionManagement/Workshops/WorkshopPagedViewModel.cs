using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.Workshops.Dtos;
using Lanpuda.ERP.ProductionManagement.Workshops.Edits;
using Lanpuda.ERP.ProductionManagement.Workshops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Lanpuda.ERP.ProductionManagement.Workshops
{
    public class WorkshopPagedViewModel : PagedViewModelBase<WorkshopDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkshopAppService _workshopAppService;


        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }


        public WorkshopPagedViewModel(IServiceProvider serviceProvider, IWorkshopAppService workshopAppService)
        {
            _serviceProvider = serviceProvider;
            _workshopAppService = workshopAppService;
            this.PageTitle = "生产车间";
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
                WorkshopEditViewModel? viewModel = _serviceProvider.GetService<WorkshopEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshAsync = this.QueryAsync;
                    WindowService.Title = "生产车间-新建";
                    WindowService.Show("WorkshopEditView", viewModel);
                }
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            WorkshopEditViewModel? viewModel = _serviceProvider.GetService<WorkshopEditViewModel>();
            if (viewModel != null)
            {
                viewModel.Model.Id = SelectedModel.Id;
                viewModel.RefreshAsync = this.QueryAsync;
                WindowService.Title = "生产车间-编辑";
                WindowService.Show("WorkshopEditView", viewModel);
            }
        }

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Name = String.Empty;
            this.Number = String.Empty;
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
                    await _workshopAppService.DeleteAsync(this.SelectedModel.Id);
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


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                WorkshopPagedRequestDto requestDto = new WorkshopPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = SkipCount;
                requestDto.Name = this.Name;
                requestDto.Number = Number;
                var result = await _workshopAppService.GetPagedListAsync(requestDto);
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
