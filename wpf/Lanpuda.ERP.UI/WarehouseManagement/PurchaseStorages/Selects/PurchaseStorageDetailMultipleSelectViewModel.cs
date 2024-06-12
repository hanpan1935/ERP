using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Selects
{
    public class PurchaseStorageDetailMultipleSelectViewModel : PagedViewModelBase<PurchaseStorageDetailSelectDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly IPurchaseStorageAppService _purchaseStorageAppService;
        public Action<ICollection<PurchaseStorageDetailSelectDto>>? SaveCallback;

        /// <summary>
        /// 右侧表格数据
        /// </summary>
        public ObservableCollection<PurchaseStorageDetailSelectDto> SelectedPurchaseStorageDetailList { get; set; }
        /// <summary>
        /// 右侧表格选中的
        /// </summary>
        public PurchaseStorageDetailSelectDto? SelectedPurchaseStorageDetail
        {
            get { return GetProperty(() => SelectedPurchaseStorageDetail); }
            set { SetProperty(() => SelectedPurchaseStorageDetail, value); }
        }


        #region 搜索

        public string PurchaseStorageNumber
        {
            get { return GetProperty(() => PurchaseStorageNumber); }
            set { SetProperty(() => PurchaseStorageNumber, value); }
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

        public string SupplierName
        {
            get { return GetProperty(() => SupplierName); }
            set { SetProperty(() => SupplierName, value); }
        }

        #endregion

        public PurchaseStorageDetailMultipleSelectViewModel(
            IServiceProvider serviceProvider,
            IPurchaseStorageAppService purchaseStorageAppService
            )
        {
            _serviceProvider = serviceProvider;
            _purchaseStorageAppService = purchaseStorageAppService;
            SelectedPurchaseStorageDetailList = new ObservableCollection<PurchaseStorageDetailSelectDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.PurchaseStorageNumber = string.Empty;
            this.ProductName = string.Empty;
            this.Batch = string.Empty;
            this.SupplierName = string.Empty;
            await QueryAsync();
        }


        [Command]
        public void Select()
        {
            if (this.SelectedModel != null)
            {
                //判断是否已经添加
                var detail = SelectedPurchaseStorageDetailList.Where(m => m.Id == SelectedModel.Id).FirstOrDefault();
                if (detail != null)
                {
                    this.SelectedPurchaseStorageDetail = detail;
                    Growl.Info("已经添加了");
                }
                else
                {
                    this.SelectedPurchaseStorageDetailList.Add(SelectedModel);
                }
            }
        }

        [Command]
        public void Delete()
        {
            if (this.SelectedPurchaseStorageDetail != null)
            {
                this.SelectedPurchaseStorageDetailList.Remove(SelectedPurchaseStorageDetail);
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
                SaveCallback(this.SelectedPurchaseStorageDetailList);
                this.Close();
            }
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                PurchaseStorageDetailPagedRequestDto requestDto = new PurchaseStorageDetailPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.ProductName = this.ProductName;
                requestDto.Batch = this.Batch;
                requestDto.PurchaseStorageNumber = this.PurchaseStorageNumber;
                requestDto.SupplierName = this.SupplierName;
                var result = await _purchaseStorageAppService.GetDetailPagedListAsync(requestDto);
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
