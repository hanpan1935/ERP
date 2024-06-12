using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Client.Common;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Selects
{
    public class SalesOrderDetailMultipleSelectViewModel : PagedViewModelBase<SalesOrderDetailSelectDto>
    {
        private readonly ISalesOrderAppService _salesOrderAppService;
        private readonly IServiceProvider _serviceProvider;
        public Action<ICollection<SalesOrderDetailSelectDto>>? OnSaveCallback;
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        #region 搜索项目
        public string SalesOrderNumber
        {
            get { return GetProperty(() => SalesOrderNumber); }
            set { SetProperty(() => SalesOrderNumber, value); }
        }

        public Guid? CustomerId
        {
            get { return GetProperty(() => CustomerId); }
            set { SetProperty(() => CustomerId, value); }
        }

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public SalesOrderType? OrderType
        {
            get { return GetProperty(() => OrderType); }
            set { SetProperty(() => OrderType, value); }
        }

        public SalesOrderCloseStatus? CloseStatus
        {
            get { return GetProperty(() => CloseStatus); }
            set { SetProperty(() => CloseStatus, value); }
        }

        public bool? IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }


        public DateTime? DeliveryDateStart
        {
            get { return GetProperty(() => DeliveryDateStart); }
            set { SetProperty(() => DeliveryDateStart, value); }
        }

        public DateTime? DeliveryDateEnd
        {
            get { return GetProperty(() => DeliveryDateEnd); }
            set { SetProperty(() => DeliveryDateEnd, value); }
        }
        #endregion


        #region 搜索数据源
        public Dictionary<string, SalesOrderType> SalesOrderTypeSource { get; set; }
        #endregion

        /// <summary>
        /// 右侧表格数据
        /// </summary>
        public ObservableCollection<SalesOrderDetailSelectDto> SelectedSalesOrderDetailList { get; set; }
       
        /// <summary>
        /// 右侧表格选中的行
        /// </summary>
        public SalesOrderDetailSelectDto? RightSelectedRow
        {
            get { return GetProperty(() => RightSelectedRow); }
            set { SetProperty(() => RightSelectedRow, value); }
        }


        /// <summary>
        /// 左侧表格选中的行
        /// </summary>
        public SalesOrderDetailSelectDto? LeftSelectedRow
        {
            get { return GetProperty(() => LeftSelectedRow); }
            set { SetProperty(() => LeftSelectedRow, value); }
        }


        public SalesOrderDetailMultipleSelectViewModel(
            ISalesOrderAppService salesOrderAppService,
            IServiceProvider serviceProvider)
        {
            _salesOrderAppService = salesOrderAppService;
            _serviceProvider = serviceProvider;
            SelectedSalesOrderDetailList = new ObservableCollection<SalesOrderDetailSelectDto>();
            SalesOrderTypeSource = EnumUtils.EnumToDictionary<SalesOrderType>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.SalesOrderNumber = String.Empty;
            //this.CustomerId == null
            this.ProductName = string.Empty;
            this.OrderType = null;
            this.CloseStatus = null;
            //this.IsConfirmed = NetTcpStyleUriParser
            this.DeliveryDateStart = null;
            this.DeliveryDateEnd = null;
            await QueryAsync();
        }

        [Command]
        public void Select()
        {
            if (this.LeftSelectedRow != null)
            {
                //判断是否已经添加
                var product = SelectedSalesOrderDetailList.Where(m => m.Id == LeftSelectedRow.Id).FirstOrDefault();
                if (product != null)
                {
                    Growl.Info("已经添加了");
                }
                else
                {
                    this.SelectedSalesOrderDetailList.Add(LeftSelectedRow);
                }
            }
        }

        [Command]
        public void Delete()
        {
            if (this.RightSelectedRow != null)
            {
                this.SelectedSalesOrderDetailList.Remove(RightSelectedRow);
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
            if (this.OnSaveCallback != null)
            {
                OnSaveCallback(this.SelectedSalesOrderDetailList);
                this.Close();
            }
        }




        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                SalesOrderDetailPagedRequestDto requestDto = new SalesOrderDetailPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.CustomerId = this.CustomerId;
                requestDto.IsConfirmed = this.IsConfirmed;
                requestDto.SalesOrderNumber = this.SalesOrderNumber;
                requestDto.ProductName = this.ProductName;
                requestDto.OrderType = this.OrderType;
                requestDto.CloseStatus = this.CloseStatus;
                requestDto.DeliveryDateStart = this.DeliveryDateStart;
                requestDto.DeliveryDateEnd = this.DeliveryDateEnd;

                var result = await _salesOrderAppService.GetDetailPagedListAsync(requestDto);
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
