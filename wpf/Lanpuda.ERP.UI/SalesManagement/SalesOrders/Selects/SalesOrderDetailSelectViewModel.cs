using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;


namespace Lanpuda.ERP.UI.SalesManagement.SalesOrders.Selects
{
    public class SalesOrderDetailSelectViewModel : PagedViewModelBase<SalesOrderDetailSelectDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        private readonly ISalesOrderAppService _salesOrderAppService;
        private readonly IObjectMapper _objectMapper;

        public Action<SalesOrderDetailSelectDto>? OnSelectedCallback { get; set; }


        public SalesOrderDetailSelectViewModel(
            ISalesOrderAppService salesOrderAppService,
            IObjectMapper objectMapper
            )
        {
            _salesOrderAppService = salesOrderAppService;
            _objectMapper = objectMapper;
        }


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

        public string CustomerName
        {
            get { return GetProperty(() => CustomerName); }
             set { SetProperty(() => CustomerName, value); }
        }
        #endregion


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

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = string.Empty;
            this.ProductName = string.Empty;
            this.CustomerName = string.Empty;
            await this.QueryAsync();
        }


        [Command]
        public void OnSelected()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            this.OnSelectedCallback?.Invoke(SelectedModel);
            if (CurrentWindowService != null)
                CurrentWindowService.Close();
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                SalesOrderDetailPagedRequestDto requestDto = new SalesOrderDetailPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.SalesOrderNumber = this.Number;
                requestDto.IsConfirmed = true;
                requestDto.ProductName = this.ProductName;
                requestDto.CustomerName = this.CustomerName;
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
