using AutoMapper.Execution;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Selects
{
    public class MpsSelectViewModel : PagedViewModelBase<MpsDto>
    {
        private readonly IMpsAppService _mpsAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;

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


        public MpsDto? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        public Action<MpsDto>? OnSelectedCallback { get; set; }

        public MpsSelectViewModel(
            IMpsAppService mpsAppService, 
            IObjectMapper objectMapper,
            IServiceProvider serviceProvider)
        {
            _mpsAppService = mpsAppService;
            _objectMapper = objectMapper;
            _serviceProvider = serviceProvider;
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }

        [Command]
        public void ResetAsync()
        {
            this.Number = string.Empty;
            this.ProductName = string.Empty;
        }

        [Command]
        public void Selected()
        {
            if (this.SelectedRow == null) return;
            this.OnSelectedCallback?.Invoke(SelectedRow);
            if (CurrentWindowService != null)
                CurrentWindowService.Close();
        }



        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                MpsPagedRequestDto requestDto = new MpsPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.IsConfirmed = true;
                requestDto.Number = this.Number;
                requestDto.ProductName = this.ProductName;
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
