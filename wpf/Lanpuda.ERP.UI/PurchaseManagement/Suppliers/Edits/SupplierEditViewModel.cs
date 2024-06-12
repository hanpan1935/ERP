using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Edits;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm;

namespace Lanpuda.ERP.PurchaseManagement.Suppliers.Edits
{
    public class SupplierEditViewModel : EditViewModelBase<SupplierEditModel>
    {
        protected IOpenFileDialogService OpenFileDialogService { get { return this.GetService<IOpenFileDialogService>(); } }

        public Func<Task>? Refresh { get; set; }
        private readonly ISupplierAppService _supplierAppService;
      

        public SupplierEditViewModel(ISupplierAppService supplierAppService)
        {
            _supplierAppService = supplierAppService;
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
                    var result = await _supplierAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.FullName = result.FullName;
                    Model.ShortName = result.ShortName;
                    Model.FactoryAddress = result.FactoryAddress;
                    Model.Contact = result.Contact;
                    Model.ContactTel = result.ContactTel;
                    Model.OrganizationName = result.OrganizationName;
                    Model.TaxNumber = result.TaxNumber;
                    Model.BankName = result.BankName;
                    Model.AccountNumber = result.AccountNumber;
                    Model.TaxAddress = result.TaxAddress;
                    Model.TaxTel = result.TaxTel;
                }
                else
                {
                    Model.Number = "系统自动生成";
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
            if (this.Refresh != null)
            {
                await Refresh();
            }
            this.Close();
        }

        public bool CanSaveAsync()
        {
            if (this.Model.HasErrors())
            {
                return false;
            }
            return true;
        }



        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                SupplierCreateDto dto = new SupplierCreateDto();
                dto.FullName = Model.FullName;
                dto.ShortName = Model.ShortName;
                dto.FactoryAddress = Model.FactoryAddress;
                dto.Contact = Model.Contact;
                dto.ContactTel = Model.ContactTel;
                dto.OrganizationName = Model.OrganizationName;
                dto.TaxNumber = Model.TaxNumber;
                dto.BankName = Model.BankName;
                dto.AccountNumber = Model.AccountNumber;
                dto.TaxAddress = Model.TaxAddress;
                dto.TaxTel = Model.TaxTel;
                await _supplierAppService.CreateAsync(dto);
                if (Refresh != null)
                {
                    await Refresh.Invoke();
                    this.CurrentWindowService.Close();
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
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }

                SupplierUpdateDto dto = new SupplierUpdateDto();
                dto.FullName = Model.FullName;
                dto.ShortName = Model.ShortName;
                dto.FactoryAddress = Model.FactoryAddress;
                dto.Contact = Model.Contact;
                dto.ContactTel = Model.ContactTel;
                dto.OrganizationName = Model.OrganizationName;
                dto.TaxNumber = Model.TaxNumber;
                dto.BankName = Model.BankName;
                dto.AccountNumber = Model.AccountNumber;
                dto.TaxAddress = Model.TaxAddress;
                dto.TaxTel = Model.TaxTel;
                await _supplierAppService.UpdateAsync((Guid)this.Model.Id, dto);
                if (Refresh != null)
                {
                    await Refresh.Invoke();
                    this.CurrentWindowService.Close();
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
    }
}
