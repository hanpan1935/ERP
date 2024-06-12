using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Client.Common;

namespace Lanpuda.ERP.WarehouseManagement.InventoryLogs
{
    public class InventoryLogPagedViewModel : PagedViewModelBase<InventoryLogDto>
    {
        private readonly IInventoryLogAppService _inventoryAppService;

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


        public DateTime? FlowTimeStart
        {
            get { return GetProperty(() => FlowTimeStart); }
            set { SetProperty(() => FlowTimeStart, value); }
        }

        public DateTime? FlowTimeEnd
        {
            get { return GetProperty(() => FlowTimeEnd); }
            set { SetProperty(() => FlowTimeEnd, value); }
        }

        public InventoryLogType? LogType
        {
            get { return GetProperty(() => LogType); }
            set { SetProperty(() => LogType, value); }
        }

        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }
        #endregion

        public Dictionary<string, InventoryLogType> InventoryLogTypeSource { get; set; }


        public InventoryLogPagedViewModel(IInventoryLogAppService inventoryAppService)
        {
            _inventoryAppService = inventoryAppService;
            this.PageTitle = "库存流水";
            InventoryLogTypeSource = EnumUtils.EnumToDictionary<InventoryLogType>();
        }


        [Command]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }

     

        [Command]
        public async Task ResetAsync()
        {
            this.Number = string.Empty;
            this.ProductName = string.Empty;
            this.FlowTimeStart = null;
            this.FlowTimeEnd = null;
            this.LogType = null;
            this.Batch = string.Empty;
            await QueryAsync();
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                InventoryLogPagedRequestDto requestDto = new InventoryLogPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.ProductName = this.ProductName;
                requestDto.FlowTimeStart = this.FlowTimeStart;
                requestDto.FlowTimeEnd = this.FlowTimeEnd;
                requestDto.LogType = this.LogType;
                requestDto.Batch = this.Batch;
                var result = await _inventoryAppService.GetPagedListAsync(requestDto);
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
