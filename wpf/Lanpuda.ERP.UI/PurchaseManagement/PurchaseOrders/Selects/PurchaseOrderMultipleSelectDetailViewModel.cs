using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.ProductCategories.Dtos;
using Lanpuda.ERP.BasicData.ProductCategories;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.BasicData.ProductUnits.Dtos;
using Lanpuda.ERP.BasicData.ProductUnits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Selects
{
    public class PurchaseOrderMultipleSelectDetailViewModel : PagedViewModelBase<PurchaseOrderDetailSelectDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        private readonly IServiceProvider _serviceProvider;
        private readonly IPurchaseOrderAppService _purchaseOrderAppService;



        public Action<ICollection<PurchaseOrderDetailSelectDto>>? SaveCallback;

        /// <summary>
        /// 右侧表格数据
        /// </summary>
        public ObservableCollection<PurchaseOrderDetailSelectDto> SelectedPurchaseOrderDetailList { get; set; }
        /// <summary>
        /// 右侧表格选中的
        /// </summary>
        public PurchaseOrderDetailSelectDto? SelectedProduct
        {
            get { return GetProperty(() => SelectedProduct); }
            set { SetProperty(() => SelectedProduct, value); }
        }

        #region 搜索

        public string PurchaseOrderNumber
        {
            get { return GetValue<string>(nameof(PurchaseOrderNumber)); }
            set { SetValue(value, nameof(PurchaseOrderNumber)); }
        }
      

        public string ProductName
        {
            get { return GetValue<string>(nameof(ProductName)); }
            set { SetValue(value, nameof(ProductName)); }
        }


        public string SupplierName
        {
            get { return GetValue<string>(nameof(SupplierName)); }
            set { SetValue(value, nameof(SupplierName)); }
        }

        #endregion



        public PurchaseOrderMultipleSelectDetailViewModel(
            IServiceProvider serviceProvider,
            IPurchaseOrderAppService purchaseOrderAppService
            )
        {
            _serviceProvider = serviceProvider;
            _purchaseOrderAppService = purchaseOrderAppService;
            SelectedPurchaseOrderDetailList = new ObservableCollection<PurchaseOrderDetailSelectDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.PurchaseOrderNumber = string.Empty;
            this.ProductName = string.Empty;
            this.SupplierName = string.Empty;
            await QueryAsync();
        }


        [Command]
        public void Select()
        {
            if (this.SelectedModel != null)
            {
                //判断是否已经添加
                var product = SelectedPurchaseOrderDetailList.Where(m => m.Id == SelectedModel.Id).FirstOrDefault();
                if (product != null)
                {
                    this.SelectedProduct = product;
                    Growl.Info("已经添加了");
                }
                else
                {
                    this.SelectedPurchaseOrderDetailList.Add(SelectedModel);
                }
            }
        }


        [Command]
        public void Delete()
        {
            if (this.SelectedProduct != null)
            {
                this.SelectedPurchaseOrderDetailList.Remove(SelectedProduct);
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
                SaveCallback(this.SelectedPurchaseOrderDetailList);
                this.Close();
            }
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                PurchaseOrderDetailPagedRequestDto requestDto = new PurchaseOrderDetailPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.IsConfirmed = true;
                requestDto.PurchaseOrderNumber = this.PurchaseOrderNumber;
                requestDto.ProductName = this.ProductName;
                requestDto.SupplierName = this.SupplierName;

                var result = await _purchaseOrderAppService.GetDetailPagedListAsync(requestDto);
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
