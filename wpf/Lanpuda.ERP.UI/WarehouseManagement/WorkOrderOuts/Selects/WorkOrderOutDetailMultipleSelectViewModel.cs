using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Selects
{
    public class WorkOrderOutDetailMultipleSelectViewModel : PagedViewModelBase<WorkOrderOutDetailSelectDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkOrderOutAppService _workOrderOutAppService;
        public Action<ICollection<WorkOrderOutDetailSelectDto>>? SaveCallback;

        /// <summary>
        /// 右侧表格数据
        /// </summary>
        public ObservableCollection<WorkOrderOutDetailSelectDto> SelectedWorkOrderOutDetailList { get; set; }
        /// <summary>
        /// 右侧表格选中的
        /// </summary>
        public WorkOrderOutDetailSelectDto? SelectedWorkOrderOutDetail
        {
            get { return GetProperty(() => SelectedWorkOrderOutDetail); }
            set { SetProperty(() => SelectedWorkOrderOutDetail, value); }
        }


        #region 搜索
        public string WorkOrderOutNumber
        {
            get { return GetProperty(() => WorkOrderOutNumber); }
            set { SetProperty(() => WorkOrderOutNumber, value); }
        }


        public string WorkOrderNumber
        {
            get { return GetProperty(() => WorkOrderNumber); }
            set { SetProperty(() => WorkOrderNumber, value); }
        }

        public string MaterialApplyNumber
        {
            get { return GetProperty(() => MaterialApplyNumber); }
            set { SetProperty(() => MaterialApplyNumber, value); }
        }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }


        #endregion

        public WorkOrderOutDetailMultipleSelectViewModel(
            IServiceProvider serviceProvider,
            IWorkOrderOutAppService workOrderOutAppService
            )
        {
            _serviceProvider = serviceProvider;
            _workOrderOutAppService = workOrderOutAppService;
            SelectedWorkOrderOutDetailList = new ObservableCollection<WorkOrderOutDetailSelectDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.WorkOrderNumber = string.Empty;
            this.WorkOrderOutNumber = string.Empty;
            this.MaterialApplyNumber = string.Empty;
            this.ProductName = string.Empty;
            await QueryAsync();
        }


        [Command]
        public void Select()
        {
            if (this.SelectedModel != null)
            {
                //判断是否已经添加
                var product = SelectedWorkOrderOutDetailList.Where(m => m.Id == SelectedModel.Id).FirstOrDefault();
                if (product != null)
                {
                    this.SelectedWorkOrderOutDetail = product;
                    Growl.Info("已经添加了");
                }
                else
                {
                    WorkOrderOutDetailSelectDto detail = new WorkOrderOutDetailSelectDto();
                    detail.Id = SelectedModel.Id;
                    detail.WorkOrderOutId = SelectedModel.WorkOrderOutId;
                    detail.WorkOrderNumber = SelectedModel.WorkOrderNumber;
                    detail.ProductId = SelectedModel.ProductId;
                    detail.ProductName = SelectedModel.ProductName;
                    detail.ProductNumber = SelectedModel.ProductNumber;
                    detail.ProductSpec = SelectedModel.ProductSpec;
                    detail.ProductUnitName= SelectedModel.ProductUnitName;
                    detail.WarehouseName = SelectedModel.WarehouseName;
                    detail.LocationName = SelectedModel.LocationName;
                    detail.LocationId = SelectedModel.LocationId;
                    detail.Batch = SelectedModel.Batch;
                    detail.Quantity = SelectedModel.Quantity;
                    this.SelectedWorkOrderOutDetailList.Add(detail);
                }
            }
        }

        [Command]
        public void Delete()
        {
            if (this.SelectedWorkOrderOutDetail != null)
            {
                this.SelectedWorkOrderOutDetailList.Remove(SelectedWorkOrderOutDetail);
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
                SaveCallback(this.SelectedWorkOrderOutDetailList);
                this.Close();
            }
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                WorkOrderOutDetailPagedRequestDto requestDto = new WorkOrderOutDetailPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.WorkOrderOutNumber = this.WorkOrderOutNumber;
                requestDto.WorkOrderNumber = this.WorkOrderNumber;
                requestDto.MaterialApplyNumber = this.MaterialApplyNumber;
                requestDto.ProductName = this.ProductName;
                var result = await _workOrderOutAppService.GetDetailPagedListAsync(requestDto);
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
