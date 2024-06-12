using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Utils;
using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
using Lanpuda.ERP.ProductionManagement.Mpses.Edits;
using Lanpuda.ERP.ProductionManagement.Mpses.Profiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.ProductionManagement.Mpses
{
    public class MpsPagedViewModel : PagedViewModelBase<MpsDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMpsAppService _mpsAppService;
        private readonly IObjectMapper _objectMapper;

        #region 搜索

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }
        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public MpsType? SelectedMpsType
        {
            get { return GetProperty(() => SelectedMpsType); }
            set { SetProperty(() => SelectedMpsType, value); }
        }


        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }

        public DateTime? StartDateStart
        {
            get { return GetProperty(() => StartDateStart); }
            set { SetProperty(() => StartDateStart, value); }
        }


        public DateTime? StartDateEnd
        {
            get { return GetProperty(() => StartDateEnd); }
            set { SetProperty(() => StartDateEnd, value); }
        }


        public DateTime? CompleteDateStart
        {
            get { return GetProperty(() => CompleteDateStart); }
            set { SetProperty(() => CompleteDateStart, value); }
        }

        public DateTime? CompleteDateEnd
        {
            get { return GetProperty(() => CompleteDateEnd); }
            set { SetProperty(() => CompleteDateEnd, value); }
        }


        public Dictionary<string, bool> IsConfirmedSource { get; set; }
        public Dictionary<string, MpsType> MpsTypeSource { get; set; }

        #endregion

        public MpsPagedViewModel(IServiceProvider serviceProvider, IMpsAppService mpsAppService, IObjectMapper objectMapper)
        {
            _serviceProvider = serviceProvider;
            _mpsAppService = mpsAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "生产计划";
            IsConfirmedSource = ItemsSoureUtils.GetBoolDictionary();
            MpsTypeSource = EnumUtils.EnumToDictionary<MpsType>();
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
                MpsEditViewModel? mpsEditViewModel = _serviceProvider.GetService<MpsEditViewModel>();
                if (mpsEditViewModel != null)
                {
                    WindowService.Title = "生产计划-新建";
                    mpsEditViewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                    mpsEditViewModel.Model.MpsType = MpsType.Internal;
                    WindowService.Show("MpsEditView", mpsEditViewModel);
                }
            }
        }


        [Command]
        public void Profile()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            MpsProfileViewModel? viewModel = _serviceProvider.GetService<MpsProfileViewModel>();
            if (viewModel != null)
            {
                WindowService.Title = "生产计划-明细";
                viewModel.MpsId = this.SelectedModel.Id;
                WindowService.Show("MpsProfileView", viewModel);
            }
        }

        public bool CanProfile()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            return true;
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
                MpsEditViewModel? mpsEditViewModel = _serviceProvider.GetService<MpsEditViewModel>();
                if (mpsEditViewModel != null)
                {
                    WindowService.Title = "生产计划-编辑";
                    mpsEditViewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                    mpsEditViewModel.Model.Id = this.SelectedModel.Id;
                    WindowService.Show("MpsEditView", mpsEditViewModel);
                }
            }
        }

        public bool CanUpdate()
        {
            if (this.SelectedModel == null) return false;
            if (this.SelectedModel.IsConfirmed != false)
            {
                return false;
            }
            return true;
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.ProductName = string.Empty; ;
            this.SelectedMpsType = null;
            this.IsConfirmed = null;

            this.StartDateStart = null;
            this.StartDateEnd = null;

            this.CompleteDateStart = null;
            this.CompleteDateEnd = null;

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
                    await _mpsAppService.DeleteAsync(SelectedModel.Id);
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

                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确认后无法编辑！", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _mpsAppService.ConfirmeAsync(this.SelectedModel.Id);
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
            return true;
        }


        [AsyncCommand]
        public async Task CreateMrpAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                this.IsLoading = true;
                await _mpsAppService.CreateMrpAsync(this.SelectedModel.Id);
                await this.QueryAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally { this.IsLoading = false; }
        }


        public bool CanCreateMrpAsync()
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
                MpsPagedRequestDto requestDto = new MpsPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.ProductName = this.ProductName;
                requestDto.IsConfirmed = this.IsConfirmed;
                requestDto.MpsType = SelectedMpsType;
                requestDto.StartDateStart = this.StartDateStart;
                requestDto.StartDateEnd = this.StartDateEnd;
                requestDto.CompleteDateStart= this.CompleteDateStart;
                requestDto.CompleteDateEnd = this.CompleteDateEnd;
                var result = await _mpsAppService.GetPagedListAsync(requestDto);
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
