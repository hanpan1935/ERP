using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;

namespace Lanpuda.ERP.WarehouseManagement.SalesOuts.Selects
{
    public class SalesOutDetailMultipleSelectViewModel : PagedViewModelBase<SalesOutDetailSelectDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly ISalesOutAppService _salesOutAppService;
        public Action<ICollection<SalesOutDetailSelectDto>>? SaveCallback;

        /// <summary>
        /// 右侧表格数据
        /// </summary>
        public ObservableCollection<SalesOutDetailSelectDto> SelectedSalesOutDetailList { get; set; }
        /// <summary>
        /// 右侧表格选中的
        /// </summary>
        public SalesOutDetailSelectDto? SelectedSalesOutDetail
        {
            get { return GetProperty(() => SelectedSalesOutDetail); }
            set { SetProperty(() => SelectedSalesOutDetail, value); }
        }


        #region 搜索

        public string SalesOutNumber
        {
            get { return GetProperty(() => SalesOutNumber); }
            set { SetProperty(() => SalesOutNumber, value); }
        }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }


        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }


        public Guid? CustomerId
        {
            get { return GetProperty(() => CustomerId); }
            set { SetProperty(() => CustomerId, value); }
        }

        #endregion

        public SalesOutDetailMultipleSelectViewModel(
            IServiceProvider serviceProvider,
            ISalesOutAppService salesOutAppService
            )
        {
            _serviceProvider = serviceProvider;
            _salesOutAppService = salesOutAppService;
            SelectedSalesOutDetailList = new ObservableCollection<SalesOutDetailSelectDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.SalesOutNumber = string.Empty;
            this.ProductName = string.Empty;
            this.Batch = string.Empty;
            this.SalesOutNumber = string.Empty;
            await QueryAsync();
        }


        [Command]
        public void Select()
        {
            if (this.SelectedModel != null)
            {
                //判断是否已经添加
                var detail = SelectedSalesOutDetailList.Where(m => m.Id == SelectedModel.Id).FirstOrDefault();
                if (detail != null)
                {
                    this.SelectedSalesOutDetail = detail;
                    Growl.Info("已经添加了");
                }
                else
                {
                    this.SelectedSalesOutDetailList.Add(SelectedModel);
                }
            }
        }

        [Command]
        public void Delete()
        {
            if (this.SelectedSalesOutDetail != null)
            {
                this.SelectedSalesOutDetailList.Remove(SelectedSalesOutDetail);
            }
        }

        [Command]
        public void Close()
        {
            if (CurrentWindowService != null)
                CurrentWindowService.Close();
        }


        [Command]
        public void Save()
        {
            if (this.SaveCallback != null)
            {
                SaveCallback(this.SelectedSalesOutDetailList);
                this.Close();
            }
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                SalesOutDetailPagedRequestDto requestDto = new SalesOutDetailPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.SalesOutNumber = this.SalesOutNumber;
                requestDto.ProductName = this.ProductName;
                requestDto.Batch = this.Batch;
                requestDto.CustomerId = this.CustomerId;
                var result = await _salesOutAppService.GetDetailPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.Clear();
                this.PagedDatas.CanNotify = false;
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
