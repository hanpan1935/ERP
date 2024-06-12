using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.OtherOuts.Edits
{
    public class OtherOutEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        [Required(ErrorMessage = "必填")]
        public string Description
        {
            get { return GetProperty(() => Description); }
            set { SetProperty(() => Description, value); }
        }


        public OtherOutDetailEditModel? SelectedDetailRow
        {
            get { return GetProperty(() => SelectedDetailRow); }
            set { SetProperty(() => SelectedDetailRow, value); }
        }

        public ObservableCollection<OtherOutDetailEditModel> Details { get; set; }

        public OtherOutEditModel()
        {
            Details = new ObservableCollection<OtherOutDetailEditModel>();
        }
    }

    public class OtherOutDetailEditModel : ModelBase
    {

        public Guid? Id { get; set; }


        public Guid InventoryId { get; set; }
        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }


        [MaxLength(128)]
        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }

        /// <summary>
        /// 出库数量
        /// </summary>
        [Required]
        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }
    
        public string ProductName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ProductNumber
        {
            get { return GetProperty(() => ProductNumber); }
            set { SetProperty(() => ProductNumber, value); }
        }

        public string ProductSpec
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public string ProductUnitName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public string WarehouseName
        {
            get { return GetProperty(() => WarehouseName); }
            set { SetProperty(() => WarehouseName, value); }
        }

        public string LocationName
        {
            get { return GetProperty(() => LocationName); }
            set { SetProperty(() => LocationName, value); }
        }
    }
}
