using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.Boms.Dtos;
using Lanpuda.ERP.ProductionManagement.Boms.Edits;
using Lanpuda.ERP.ProductionManagement.Boms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using Volo.Abp.ObjectMapping;
using System.Drawing;
using Lanpuda.ERP.ProductionManagement.Mpses.Edits;
using Lanpuda.ERP.ProductionManagement.Mpses;
using Microsoft.Extensions.DependencyInjection;

namespace Lanpuda.ERP.ProductionManagement.Boms
{
    public class BomPagedViewModel : PagedViewModelBase<BomDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBomAppService _bomAppService;
        private readonly IObjectMapper _objectMapper;
        public Dictionary<string, bool> IsActiveSource { get; set; }


        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }
   

        public BomPagedViewModel(
            IServiceProvider serviceProvider,
            IBomAppService bomAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _bomAppService = bomAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "生产BOM";
            IsActiveSource = new Dictionary<string, bool>();
            IsActiveSource.Add("是", true);
            IsActiveSource.Add("否", false);

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
                BomEditViewModel? viewModel = _serviceProvider.GetService<BomEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                    WindowService.Title = "生产BOM-新建";
                    WindowService.Show("BomEditView", viewModel);
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
            if (this.WindowService != null)
            {
                BomEditViewModel? viewModel = _serviceProvider.GetService<BomEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                    WindowService.Title = "生产BOM-编辑";
                    WindowService.Show("BomEditView", viewModel);
                }
            }
        }


        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            return true;
        }



        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.ProductName = String.Empty;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _bomAppService.DeleteAsync(SelectedModel.Id);
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

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                BomPagedRequestDto requestDto = new BomPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.ProductName = this.ProductName;

                var result = await _bomAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                PagedDatas.CanNotify = true;
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
