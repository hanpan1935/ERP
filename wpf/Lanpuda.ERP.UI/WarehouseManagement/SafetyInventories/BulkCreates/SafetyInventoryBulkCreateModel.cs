using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.SafetyInventories.BulkCreates
{
    public class SafetyInventoryBulkCreateModel : ModelBase
    {
        public Guid ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId, value); }
        }

        public string ProductNumber
        {
            get { return GetProperty(() => ProductNumber); }
            set { SetProperty(() => ProductNumber, value); }
        }
        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }
        public string ProductSpec
        {
            get { return GetProperty(() => ProductSpec); }
            set { SetProperty(() => ProductSpec, value); }
        }
        public string ProductUnitName
        {
            get { return GetProperty(() => ProductUnitName); }
            set { SetProperty(() => ProductUnitName, value); }
        }

        public double? MinQuantity
        {
            get { return GetProperty(() => MinQuantity); }
            set { SetProperty(() => MinQuantity, value); }
        }

        public double? MaxQuantity
        {
            get { return GetProperty(() => MaxQuantity); }
            set { SetProperty(() => MaxQuantity, value); }
        }
    }
}
