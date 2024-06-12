using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lanpuda.ERP.SalesManagement.Customers.Edits;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;

namespace Lanpuda.ERP.SalesManagement.Customers
{
    public class CustomerPagedViewModel : PagedViewModelBase<CustomerDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICustomerAppService _customerAppService;



        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string FullName
        {
            get { return GetProperty(() => FullName); }
            set { SetProperty(() => FullName, value); }
        }


        public CustomerPagedViewModel(IServiceProvider serviceProvider, ICustomerAppService customerAppService)
        {
            _serviceProvider = serviceProvider;
            _customerAppService = customerAppService;
            this.PageTitle = "客户信息";

        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            if (this.WindowService != null)
            {
                CustomerEditViewModel? customerCreateViewModel = (CustomerEditViewModel?)_serviceProvider.GetService(typeof(CustomerEditViewModel));
                if (customerCreateViewModel != null)
                {
                    customerCreateViewModel.Refresh = this.QueryAsync;
                    WindowService.Title = "客户信息-新建";
                    WindowService.Show("CustomerEditView", customerCreateViewModel);
                }
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null) { return; }
            if (this.WindowService != null)
            {
                CustomerEditViewModel? customerEditViewModel = (CustomerEditViewModel?)_serviceProvider.GetService(typeof(CustomerEditViewModel));
                if (customerEditViewModel != null)
                {
                    customerEditViewModel.Model.Id = this.SelectedModel.Id;
                    customerEditViewModel.Refresh = this.QueryAsync;
                    WindowService.Title = "客户信息-编辑";
                    WindowService.Show("CustomerEditView", customerEditViewModel);
                }
            }
        }



        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.FullName = String.Empty;
            await QueryAsync();
        }

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _customerAppService.DeleteAsync(this.SelectedModel.Id);
                    await this.QueryAsync();
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


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                CustomerPagedRequestDto requestDto = new CustomerPagedRequestDto();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount =  this.SkipCount;
                requestDto.Number = this.Number;
                requestDto.FullName = this.FullName;
                var result = await _customerAppService.GetPagedListAsync(requestDto);
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
