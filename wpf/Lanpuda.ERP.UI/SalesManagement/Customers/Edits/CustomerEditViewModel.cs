using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.SalesManagement.Customers.Edits
{
    public class CustomerEditViewModel : EditViewModelBase<CustomerEditModel>
    {
        public Func<Task>? Refresh { get; set; }
        private readonly ICustomerAppService _customerAppService;
        private readonly IObjectMapper _objectMapper;

        public CustomerEditViewModel(ICustomerAppService customerAppService, IObjectMapper objectMapper)
        {
            _customerAppService = customerAppService;
            _objectMapper = objectMapper;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _customerAppService.GetAsync(id);
                    Model = _objectMapper.Map<CustomerDto, CustomerEditModel>(result);
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



        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null)
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
                CustomerCreateDto dto = _objectMapper.Map<CustomerEditModel, CustomerCreateDto>(this.Model);
                //dto.Number = Model.Number;
                //dto.FullName = Model.FullName;
                //dto.ShortName = Model.ShortName;
                //dto.Contact = Model.Contact;
                //dto.ContactTel = Model.ContactTel;
                //dto.ShippingAddress = Model.ShippingAddress;
                //dto.Consignee = Model.Consignee;
                //dto.ConsigneeTel = Model.ConsigneeTel;
                //dto.OrganizationName = Model.OrganizationName;
                //dto.TaxNumber = Model.TaxNumber;
                //dto.BankName = Model.BankName;
                //dto.AccountNumber = Model.AccountNumber;
                //dto.TaxAddress = Model.TaxAddress;
                //dto.TaxTel = Model.TaxTel;
                //dto.Description = Model.Description;
                await _customerAppService.CreateAsync(dto);
                if (Refresh != null)
                {
                    await Refresh();
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
                CustomerUpdateDto dto = _objectMapper.Map<CustomerEditModel, CustomerUpdateDto>(this.Model);
                //dto.Number = Model.Number;
                //dto.FullName = Model.FullName;
                //dto.ShortName = Model.ShortName;
                //dto.Contact = Model.Contact;
                //dto.ContactTel = Model.ContactTel;
                //dto.ShippingAddress = Model.ShippingAddress;
                //dto.Consignee = Model.Consignee;
                //dto.ConsigneeTel = Model.ConsigneeTel;
                //dto.OrganizationName = Model.OrganizationName;
                //dto.TaxNumber = Model.TaxNumber;
                //dto.BankName = Model.BankName;
                //dto.AccountNumber = Model.AccountNumber;
                //dto.TaxAddress = Model.TaxAddress;
                //dto.TaxTel = Model.TaxTel;
                //dto.Description = Model.Description;
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _customerAppService.UpdateAsync((Guid)this.Model.Id, dto);
                if (Refresh != null)
                {
                    await Refresh();
                    this.Close();
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
    }
}
