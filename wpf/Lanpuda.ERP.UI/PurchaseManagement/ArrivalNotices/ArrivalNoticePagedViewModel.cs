using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Edits;
using Microsoft.Extensions.DependencyInjection;
using NUglify.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.PurchaseManagement.ArrivalNotices
{
    public class ArrivalNoticePagedViewModel : PagedViewModelBase<ArrivalNoticeDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IArrivalNoticeAppService _arrivalNoticeAppService;
        private readonly IObjectMapper _objectMapper;

        public ArrivalNoticePagedRequestModel RequestModel
        {
            get { return GetProperty(() => RequestModel); }
            set { SetProperty(() => RequestModel, value); }
        }

        public ArrivalNoticePagedViewModel(
            IServiceProvider serviceProvider,
            IArrivalNoticeAppService arrivalNoticeAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _arrivalNoticeAppService = arrivalNoticeAppService;
            _objectMapper = objectMapper;
            RequestModel = new ArrivalNoticePagedRequestModel();
            this.PageTitle = "来料通知";
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            ArrivalNoticeEditViewModel? arrivalNoticeEditViewModel = _serviceProvider.GetService<ArrivalNoticeEditViewModel>();
            if (arrivalNoticeEditViewModel != null)
            {
                WindowService.Title = "来料通知-新建";
                arrivalNoticeEditViewModel.OnCloseWindowCallbackAsync = OnEditViewBackAsync;
                WindowService.Show("ArrivalNoticeEditView", arrivalNoticeEditViewModel);
            }
        }

        [Command]
        public void Update()
        {
            if (this.SelectedModel != null)
            {
                ArrivalNoticeEditViewModel? arrivalNoticeEditViewModel = _serviceProvider.GetService<ArrivalNoticeEditViewModel>();
                if (arrivalNoticeEditViewModel != null)
                {
                    WindowService.Title = "来料通知-编辑";
                    arrivalNoticeEditViewModel.OnCloseWindowCallbackAsync = OnEditViewBackAsync;
                    arrivalNoticeEditViewModel.Model.Id = this.SelectedModel.Id;
                    //arrivalNoticeEditViewModel.i
                    WindowService.Show("ArrivalNoticeEditView", arrivalNoticeEditViewModel);
                }
            }
        }

        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            var entity = this.PagedDatas.Where(x => x.Id == SelectedModel.Id).FirstOrDefault();
            if (entity == null)
            {
                return false;
            }
            if (entity.IsConfirmed != false)
            {
                return false;
            }
            return true;
        }



        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.RequestModel.Number = string.Empty;
            RequestModel.ArrivalTimeStart = null;
            RequestModel.ArrivalTimeEnd = null;
            RequestModel.IsInspect = null;
            RequestModel.IsConfirmed = null;
            await QueryAsync();
        }

        [Command]
        public async Task DeleteAsync()
        {
            try
            {
                if (this.SelectedModel == null) return;
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _arrivalNoticeAppService.DeleteAsync(this.SelectedModel.Id);
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
            if (SelectedModel == null)
            {
                return false;
            }
            return true;
        }




        [AsyncCommand]
        public async Task ConfirmeAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确认后无法修改", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    Guid id = this.SelectedModel.Id;
                    await _arrivalNoticeAppService.ConfirmeAsync(id);
                    await this.QueryAsync();
                }
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

        public bool CanConfirmeAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsConfirmed != false)
            {
                return false;
            }

            if (this.SelectedModel.Details.Count == 0)
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
                ArrivalNoticePagedRequestDto requestDto = new ArrivalNoticePagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.RequestModel.Number;
                requestDto.IsConfirmed = this.RequestModel.IsConfirmed;
                requestDto.ArrivalTimeStart = this.RequestModel.ArrivalTimeStart;
                requestDto.ArrivalTimeEnd = this.RequestModel.ArrivalTimeEnd;
                var result = await _arrivalNoticeAppService.GetPagedListAsync(requestDto);
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

        private async Task OnEditViewBackAsync()
        {
            await QueryAsync();
        }
    }
}
