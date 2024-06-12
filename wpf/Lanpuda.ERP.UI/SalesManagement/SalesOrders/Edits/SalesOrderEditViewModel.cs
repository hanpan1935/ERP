using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelectWithSalesPrice;
using Lanpuda.ERP.SalesManagement.Customers;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Edits
{
    public class SalesOrderEditViewModel : EditViewModelBase<SalesOrderEditModel>
    {
        private readonly ISalesOrderAppService _salesOrderAppService;
        private readonly ICustomerAppService _customerLookupAppService;
        private readonly IServiceProvider _serviceProvider;


        public ObservableCollection<CustomerLookupDto> CustomerSource { get; set; }
        public Dictionary<string, SalesOrderType> OrderTypeSource { get; set; }

        public Func<Task>? RefreshCallbackAsync { get; set; }

        public SalesOrderEditViewModel(
            ISalesOrderAppService salesOrderAppService,
            ICustomerAppService customerLookupAppService,
            IServiceProvider serviceProvider)
        {
            _salesOrderAppService = salesOrderAppService;
            _customerLookupAppService = customerLookupAppService;
            _serviceProvider = serviceProvider;
            Model = new SalesOrderEditModel();
            CustomerSource = new ObservableCollection<CustomerLookupDto>();
            OrderTypeSource = EnumUtils.EnumToDictionary<SalesOrderType>();
        }
        

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var customers = await _customerLookupAppService.GetAllAsync();

                foreach (var item in customers)
                {
                    CustomerSource.Add(item);
                }

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "销售订单-编辑";
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _salesOrderAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.CustomerId = result.CustomerId;
                    Model.RequiredDate = result.RequiredDate;
                    Model.PromisedDate = result.PromisedDate;
                    Model.OrderType = result.OrderType;
                    Model.Description = result.Description;
                    foreach (var detail in result.Details)
                    {
                        SalesOrderDetailEditModel detailModel = new SalesOrderDetailEditModel();
                        detailModel.ProductId = detail.ProductId;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductNumber = detail.ProductNumber;
                        detailModel.ProductSpec= detail.ProductSpec;
                        detailModel.ProductUnitName = detail.ProductUnitName;
                        detailModel.Price = detail.Price;
                        detailModel.TaxRate = detail.TaxRate;
                        detailModel.DeliveryDate = detail.DeliveryDate;
                        detailModel.Quantity = detail.Quantity;
                        detailModel.Requirement = detail.Requirement;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "销售订单-新建";
                    this.Model.Number = "系统自动生成";
                    this.Model.RequiredDate = DateTime.Now;
                    this.Model.PromisedDate = DateTime.Now;
                }
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


        [Command]
        public void ShowSelectProductWindow()
        {
            ProductMultipleSelectWithSalesPriceViewModel? viewModel = _serviceProvider.GetService<ProductMultipleSelectWithSalesPriceViewModel>();
            if (viewModel != null)
            {
                viewModel.SaveCallback = this.OnProductSelected;
                viewModel.CustomerId = this.Model.CustomerId;
                this.WindowService.Title = "请选择产品";
                this.WindowService.Show("ProductMultipleSelectWithSalesPriceView", viewModel);
            }
        }

        [Command]
        public void DeleteSelectedRow()
        {
            if (Model.SelectedDetailEntity != null)
            {
                this.Model.Details.Remove(Model.SelectedDetailEntity);
            }
        }

        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null || Model.Id == Guid.Empty)
            {
                await CreateAsync();
            }
            else
            {
                await UpdateAsync();
            }
        }

        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }

        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                SalesOrderCreateDto dto = new SalesOrderCreateDto();
                dto.CustomerId = Model.CustomerId;
                dto.RequiredDate = Model.RequiredDate;
                dto.PromisedDate = Model.PromisedDate;
                dto.OrderType = Model.OrderType;
                dto.Description = Model.Description;

                for (int i = 0; i < Model.Details.Count; i++)
                {
                    SalesOrderDetailCreateDto detailDto = new SalesOrderDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Price = detail.Price;
                    detailDto.TaxRate = detail.TaxRate;
                    detailDto.DeliveryDate = detail.DeliveryDate;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.Requirement = detail.Requirement;
                    dto.Details.Add(detailDto);
                }
                await _salesOrderAppService.CreateAsync(dto);
                CurrentWindowService.Close();
                if (RefreshCallbackAsync != null)
                {
                    await RefreshCallbackAsync();
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

        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                SalesOrderUpdateDto dto = new SalesOrderUpdateDto();
                dto.CustomerId = Model.CustomerId;
                dto.RequiredDate = Model.RequiredDate;
                dto.PromisedDate = Model.PromisedDate;
                dto.OrderType = Model.OrderType;
                dto.Description = Model.Description;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    SalesOrderDetailUpdateDto detailDto = new SalesOrderDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Price = detail.Price;
                    detailDto.TaxRate = detail.TaxRate;
                    detailDto.DeliveryDate = detail.DeliveryDate;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.Requirement = detail.Requirement;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _salesOrderAppService.UpdateAsync((Guid)Model.Id, dto);
                CurrentWindowService.Close();
                if (RefreshCallbackAsync != null)
                {
                    await RefreshCallbackAsync();
                }
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

        private void OnProductSelected(ICollection<ProductWithPriceDto> products)
        {
            foreach (var item in products)
            {
                SalesOrderDetailEditModel detailEditModel = new SalesOrderDetailEditModel();
                detailEditModel.DeliveryDate = DateTime.Now;
                detailEditModel.Quantity = 0;
                detailEditModel.ProductId = item.Id;
                detailEditModel.ProductName = item.Name;
                detailEditModel.ProductNumber = item.Number;
                detailEditModel.ProductSpec = item.Spec;
                detailEditModel.ProductUnitName = item.ProductUnitName;
                if (item.Price != null)
                {
                    detailEditModel.Price = (double)item.Price;
                }
                if (item.TaxRate != null)
                {
                    detailEditModel.TaxRate = (double)item.TaxRate;
                }
                detailEditModel.Requirement = string.Empty;
                this.Model.Details.Add(detailEditModel);
            }
        }
    }
}

