using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.ERP.SalesManagement.Customers;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using System.DirectoryServices.ActiveDirectory;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.ERP.SalesManagement.SalesPrices.Dtos;
using Volo.Abp.ObjectMapping;
using System.Windows.Media.Animation;
using Lanpuda.ERP.BasicData.Products.Selects.MultipleSelect;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Mvvm;
using JetBrains.Annotations;
using Lanpuda.ERP.UI.SalesManagement.SalesPrices.Selects;

namespace Lanpuda.ERP.SalesManagement.SalesPrices.Edits
{
    public class SalesPriceEditViewModel : EditViewModelBase<SalesPriceEditModel>
    {
        private readonly ISalesPriceAppService _salesPriceAppService;
        private readonly ICustomerAppService _customerLookupAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;

        public Func<Task>? RefreshCallbackAsync { get; set; }

        public ObservableCollection<CustomerLookupDto> CustomerSource { get; set; }
        private List<CustomerLookupDto> CustomerList { get; set; }

        public string CustomerSearchText
        {
            get { return GetValue<string>(); }
            set { SetValue(value, OnCustomerSearchTextChanged); }
        }


        public SalesPriceEditViewModel(
            ISalesPriceAppService salesPriceAppService,
            ICustomerAppService customerLookupAppService,
            IObjectMapper objectMapper,
            IServiceProvider serviceProvider
            )
        {
            _salesPriceAppService = salesPriceAppService;
            _customerLookupAppService = customerLookupAppService;
            _objectMapper = objectMapper;
            _serviceProvider = serviceProvider;
            CustomerSource = new ObservableCollection<CustomerLookupDto>();
            CustomerList = new List<CustomerLookupDto>();
        }

    

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                CustomerList = await _customerLookupAppService.GetAllAsync();
                foreach (var item in CustomerList)
                {
                    CustomerSource.Add(item);
                }

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "销售报价-编辑";
                    Guid id = (Guid)Model.Id;
                    var result = await _salesPriceAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.Customer = CustomerList.Where(m=>m.Id == result.CustomerId).First();
                    Model.ValidDate = result.ValidDate;
                    Model.Remark= result.Remark;
                    foreach (var item in result.Details)
                    {
                        SalesPriceDetailEditModel detailEditModel = new SalesPriceDetailEditModel();
                        detailEditModel.ProductId = item.ProductId;
                        detailEditModel.ProductNumber = item.ProductNumber;
                        detailEditModel.ProductName = item.ProductName;
                        detailEditModel.ProductSpec = item.ProductSpec;
                        detailEditModel.ProductUnitName = item.ProductUnitName;
                        detailEditModel.Price= item.Price;
                        detailEditModel.TaxRate = item.TaxRate;
                        Model.Details.Add(detailEditModel);
                    }


                }
                else 
                {
                    this.PageTitle = "销售报价-新建";
                    this.Model.Number = "系统自动生成";
                    this.Model.ValidDate = DateTime.Now;
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
        public void InsertRowBelow()
        {
            SalesPriceDetailEditModel detail = new SalesPriceDetailEditModel();
            this.Model.Details.Add(detail);
        }


        [Command]
        public void DeleteSelectedRow()
        {
            if (Model.SelectedDetailEntity != null)
            {
                this.Model.Details.Remove(Model.SelectedDetailEntity);
            }
        }


        [Command]
        public void ShowSelectProductWindow()
        {
            ProductMultipleSelectViewModel? viewModel = _serviceProvider.GetService<ProductMultipleSelectViewModel>();
            if (viewModel != null)
            {
                viewModel.SaveCallback = OnSelectedProductsCallback;
                this.WindowService.Title = "请选择产品";
                this.WindowService.Show("ProductMultipleSelectView", viewModel);
            }
        }

        public void OnSelectedProductsCallback(ICollection<ProductDto> productList)
        {
            foreach (var item in productList)
            {
                SalesPriceDetailEditModel detailEditModel = new SalesPriceDetailEditModel();
                detailEditModel.ProductId = item.Id;
                detailEditModel.ProductNumber = item.Number;
                detailEditModel.ProductName= item.Name;
                detailEditModel.ProductSpec = item.Spec;
                detailEditModel.ProductUnitName = item.ProductUnitName;
                detailEditModel.Price = 0;
                detailEditModel.TaxRate = 0;
                this.Model.Details.Add(detailEditModel);
            }
        }



        [Command]
        public void ShowSelectSalesPriceWindow()
        {
            SalesPriceSelectViewModel? viewModel = _serviceProvider.GetService<SalesPriceSelectViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectCallback = OnSelectedSalesPriceCallback;
                this.WindowService.Title = "请选择报价单";
                this.WindowService.Show("SalesPriceSelectView", viewModel);
            }
        }


        private void OnSelectedSalesPriceCallback(SalesPriceDto salesPrice)
        {
            foreach (var item in salesPrice.Details)
            {
                SalesPriceDetailEditModel editModel = new SalesPriceDetailEditModel();
                editModel.ProductId = item.ProductId;
                editModel.ProductNumber = item.ProductNumber;
                editModel.ProductName = item.ProductName;
                editModel.ProductSpec = item.ProductSpec;
                editModel.ProductUnitName = item.ProductUnitName;
                editModel.Price = item.Price; 
                editModel.TaxRate = item.TaxRate;
                this.Model.Details.Add(editModel);
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
                SalesPriceCreateDto dto = new SalesPriceCreateDto();
                if (Model.Customer != null)
                {
                    dto.CustomerId = Model.Customer.Id;
                }    
                dto.ValidDate = Model.ValidDate;
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    var item = Model.Details[i];
                    SalesPriceDetailCreateDto detailDto = new SalesPriceDetailCreateDto();
                    detailDto.Sort = i;
                    detailDto.ProductId = item.ProductId;
                    detailDto.Price = item.Price;
                    detailDto.TaxRate = item.TaxRate;
                    dto.Details.Add(detailDto);
                }
                await _salesPriceAppService.CreateAsync(dto);
                if (RefreshCallbackAsync != null)
                {
                    await RefreshCallbackAsync();
                    this.Close();
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
                SalesPriceUpdateDto dto = new SalesPriceUpdateDto();
                if (Model.Customer != null)
                {
                    dto.CustomerId = Model.Customer.Id;
                }
                dto.ValidDate = Model.ValidDate;
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    var item = Model.Details[i];
                    SalesPriceDetailUpdateDto detailDto = new SalesPriceDetailUpdateDto();
                    detailDto.Sort = i;
                    detailDto.ProductId = item.ProductId;
                    detailDto.Price = item.Price;
                    detailDto.TaxRate = item.TaxRate;
                    dto.Details.Add(detailDto);
                }

                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _salesPriceAppService.UpdateAsync((Guid)Model.Id, dto);
                if (RefreshCallbackAsync != null)
                {
                    await RefreshCallbackAsync();
                    this.Close();
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

        private void OnCustomerSearchTextChanged()
        {
            if (!this.CustomerSearchText.IsNullOrEmpty())
            {
                var list = this.CustomerList.Where(m => m.FullName.Contains(CustomerSearchText)).ToList();

                this.CustomerSource.Clear();
                foreach (var item in list)
                {
                    CustomerSource.Add(item);
                }
            }
            else
            {
                this.CustomerSource.Clear();
                foreach (var item in CustomerList)
                {
                    CustomerSource.Add(item);
                }
            }
        }
    }
}

