using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Edits;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using DevExpress.Mvvm.UI;
using Lanpuda.Client.Theme.Utils;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies
{
    public class MaterialReturnApplyPagedViewModel : PagedViewModelBase<MaterialReturnApplyDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMaterialReturnApplyAppService _materialReturnApplyAppService;


        #region 搜索

       
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

   

        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }

        public Dictionary<string,bool> IsConfirmedSource { get; set; }
        #endregion
        public MaterialReturnApplyPagedViewModel(IServiceProvider serviceProvider, IMaterialReturnApplyAppService materialReturnApplyAppService)
        {
            _serviceProvider = serviceProvider;
            _materialReturnApplyAppService = materialReturnApplyAppService;
            this.PageTitle = "退料申请";
            IsConfirmedSource = ItemsSoureUtils.GetBoolDictionary();
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
                MaterialReturnApplyEditViewModel? viewModel = _serviceProvider.GetService<MaterialReturnApplyEditViewModel>();
                if (viewModel != null)
                {
                    WindowService.Title = "退料申请-新建";
                    viewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                    WindowService.Show("MaterialReturnApplyEditView", viewModel);
                }
            }
        }


        [Command]
        public void Update()
        {
            if (SelectedModel == null) return;
            if (this.WindowService != null)
            {
                MaterialReturnApplyEditViewModel? viewModel = _serviceProvider.GetService<MaterialReturnApplyEditViewModel>();
                if (viewModel != null)
                {
                    WindowService.Title = "退料申请-新建";
                    viewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                    viewModel.Model.Id = this.SelectedModel.Id;
                    WindowService.Show("MaterialReturnApplyEditView", viewModel);
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
            this.Number = String.Empty;
            this.IsConfirmed = null;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            if (this.SelectedModel == null) return;
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _materialReturnApplyAppService.DeleteAsync(this.SelectedModel.Id);
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
                if (this.SelectedModel == null)
                {
                    return;
                }

                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确认后无法修改!", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _materialReturnApplyAppService.ConfirmeAsync(this.SelectedModel.Id);
                    await this.QueryAsync();
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
                MaterialReturnApplyPagedRequestDto requestDto = new MaterialReturnApplyPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.IsConfirmed = this.IsConfirmed;
                var result = await _materialReturnApplyAppService.GetPagedListAsync(requestDto);
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
