//using DevExpress.Mvvm;
//using DevExpress.Mvvm.DataAnnotations;
//using Lanpuda.Client.Mvvm;
//using Lanpuda.ERP.ProductionManagement.Mpses;
//using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Threading.Tasks;

//namespace Lanpuda.ERP.SalesManagement.SalesOrders.CreateMpses
//{
//    public class SalesOrderCreateMpsViewModel : ModalViewModelBase<ObservableCollection<SalesOrderCreateMpsModel>>
//    {
//        private readonly ISalesOrderAppService _salesOrderAppService;
//        private readonly IServiceProvider _serviceProvider;
//        private readonly IMpsAppService _mpsAppService;
//        public Guid SalesOrderId { get; set; }

//        public SalesOrderCreateMpsModel? SelectedRow
//        {
//            get { return GetProperty(() => SelectedRow); }
//            set { SetProperty(() => SelectedRow, value); }
//        }


//        public Func<Task>? RefreshCallbackAsync { get; set; }

//        public SalesOrderCreateMpsViewModel(
//            ISalesOrderAppService salesOrderAppService,
//            IMpsAppService mpsAppService,
//            IServiceProvider serviceProvider)
//        {
//            _salesOrderAppService = salesOrderAppService;
//            _serviceProvider = serviceProvider;
//            _mpsAppService = mpsAppService;
//        }


//        [AsyncCommand]
//        public async Task InitializeAsync()
//        {
//            try
//            {
//                this.IsLoading = true;
//                var result = await _salesOrderAppService.GetAsync(SalesOrderId);
//                foreach (var item in result.Details)
//                {
//                    SalesOrderCreateMpsModel detailModel = new SalesOrderCreateMpsModel();
//                    detailModel.ProductId = item.ProductId;
//                    detailModel.SalesOrderDetailId = item.Id;
//                    detailModel.ProductNumber = item.ProductNumber;
//                    detailModel.ProductName = item.ProductName;
//                    detailModel.ProductSpec = item.ProductSpec;
//                    detailModel.ProductUnitName = item.ProductUnitName;
//                    detailModel.OrderQuantity = item.Quantity;
//                    detailModel.DeliveryDate = item.DeliveryDate;
//                    detailModel.StartTime = DateTime.Now;
//                    detailModel.CompleteTime = item.DeliveryDate;
//                    detailModel.Remark = item.Requirement;
//                    detailModel.Quantity = item.Quantity;
//                    this.Model.Add(detailModel);
//                }

//            }
//            catch (Exception e)
//            {
//                HandleException(e);
//            }
//            finally
//            {
//                this.IsLoading = false;
//            }
//        }


//        [Command]
//        public void DeleteSelectedRow()
//        {
//            if (SelectedRow != null)
//            {
//                this.Model.Remove(SelectedRow);
//            }
//        }

//        [AsyncCommand]
//        public async Task SaveAsync()
//        {
//            await CreateAsync();
//        }

//        public bool CanSaveAsync()
//        {
//            foreach (var item in Model)
//            {
//                if (item.HasErrors())
//                {
//                    return false;
//                }
//            }

//            if (Model.Count <= 0)
//            {
//                return false;
//            }
//            return true;
//        }

//        private async Task CreateAsync()
//        {
//            try
//            {
//                this.IsLoading = true;
//                List<MpsCreateDto> mpsList = new List<MpsCreateDto>();
//                foreach (var item in Model)
//                {
//                    MpsCreateDto dto = new MpsCreateDto();
//                    dto.SalesOrderDetailId = item.SalesOrderDetailId;
//                    dto.StartDate = item.StartTime;
//                    dto.CompleteDate = item.CompleteTime;
//                    dto.ProductId = item.ProductId;
//                    dto.Quantity = item.Quantity;
//                    dto.Remark = item.Remark;
//                    mpsList.Add(dto);
//                }
//                await _mpsAppService.BulkCreateFromSalesOrderAsync(mpsList);
//                CurrentWindowService.Close();
//                if (RefreshCallbackAsync != null)
//                {
//                    await RefreshCallbackAsync();
//                }
//            }
//            catch (Exception e)
//            {
//                HandleException(e);
//                throw;
//            }
//            finally
//            {
//                this.IsLoading = false;
//            }
//        }
//    }
//}

