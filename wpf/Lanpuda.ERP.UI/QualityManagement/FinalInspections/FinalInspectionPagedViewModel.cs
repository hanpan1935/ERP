using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.QualityManagement.FinalInspections.Dtos;
using Lanpuda.ERP.QualityManagement.FinalInspections.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.ERP.QualityManagement.FinalInspections
{
    public class FinalInspectionPagedViewModel : PagedViewModelBase<FinalInspectionDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFinalInspectionAppService _finalInspectionAppService;

        #region 搜索

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string MpsNumber
        {
            get { return GetProperty(() => MpsNumber); }
            set { SetProperty(() => MpsNumber, value); }
        }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }
        #endregion


        public FinalInspectionPagedViewModel(
                IServiceProvider serviceProvider,
                IFinalInspectionAppService finalInspectionAppService
                )
        {
            _serviceProvider = serviceProvider;
            _finalInspectionAppService = finalInspectionAppService;
            this.PageTitle = "产品终检";
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
        public void Update()
        {
            if (this.WindowService != null)
            {
                FinalInspectionEditViewModel? viewModel = _serviceProvider.GetService<FinalInspectionEditViewModel>();
                if (viewModel != null && SelectedModel != null)
                {
                    viewModel.Model.Id = SelectedModel.Id;
                    viewModel.OnCloseCallbackAsync = this.QueryAsync;
                    WindowService.Title = "采购报价-编辑";
                    WindowService.Show("FinalInspectionEditView", viewModel);
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
        public async Task ConfirmeAsync()
        {
            try
            {
                if (this.SelectedModel == null) return;
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确认后无法修改", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _finalInspectionAppService.ConfirmeAsync(this.SelectedModel.Id);
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
                FinalInspectionPagedRequestDto requestDto = new FinalInspectionPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.MpsNumber = this.MpsNumber;
                requestDto.ProductName = this.ProductName;
                var result = await _finalInspectionAppService.GetPagedListAsync(requestDto);
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
