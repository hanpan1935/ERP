using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.PurchaseManagement.PurchasePrices
{
    public class PurchasePricePagedRequestModel : ModelBase
    {
        public Guid? SupplierId
        {
            get { return GetProperty(() => SupplierId); }
            set { SetProperty(() => SupplierId, value); }
        }

        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public DateTime? QuotationDateStart
        {
            get { return GetProperty(() => QuotationDateStart); }
            set { SetProperty(() => QuotationDateStart, value); }
        }

        public DateTime? QuotationDateEnd
        {
            get { return GetProperty(() => QuotationDateEnd); }
            set { SetProperty(() => QuotationDateEnd, value); }
        }


        public ObservableCollection<SupplierDto> SupplierSource
        {
            get { return GetProperty(() => SupplierSource); }
            set { SetProperty(() => SupplierSource, value); }
        }


        public PurchasePricePagedRequestModel()
        {
            SupplierSource = new ObservableCollection<SupplierDto>();
        }

    }
}
