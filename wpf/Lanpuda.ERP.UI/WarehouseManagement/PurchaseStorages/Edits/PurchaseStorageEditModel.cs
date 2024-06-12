using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Edits
{
    public class PurchaseStorageEditModel : ModelBase
    {

        public Guid Id
        {
            get { return GetProperty(() => Id); }
            set { SetProperty(() => Id, value); }
        }

        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string? ArrivalNoticeNumber
        {
            get { return GetProperty(() => ArrivalNoticeNumber); }
            set { SetProperty(() => ArrivalNoticeNumber, value); }
        }

        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }


        public Guid ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId, value); }
        }
 

        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public string? ProductUnitName
        {
            get { return GetProperty(() => ProductUnitName); }
            set { SetProperty(() => ProductUnitName, value); }
        }

        public Guid? ProductDefaultWarehouseId { get; set; }


        public Guid? ProductDefaultLocationId { get; set; }


        /// <summary>
        /// 通知单数量
        /// </summary>
        public double ApplyQuantity
        {
            get { return GetProperty(() => ApplyQuantity); }
            set { SetProperty(() => ApplyQuantity, value); }
        }



        public ObservableCollection<WarehouseDto> WarehouseSource
        {
            get { return GetProperty(() => WarehouseSource); }
            set { SetProperty(() => WarehouseSource, value); }
        }



        public PurchaseStorageDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        public ObservableCollection<PurchaseStorageDetailEditModel> Details
        {
            get { return GetProperty(() => Details); }
            set { SetProperty(() => Details, value); }
        }

        public PurchaseStorageEditModel()
        {
            Details = new ObservableCollection<PurchaseStorageDetailEditModel>();
            WarehouseSource = new ObservableCollection<WarehouseDto>();
        }


        public double TotalQuantity
        {
            get
            {
                var res = Details.Sum(m => m.Quantity);
                return res;
            }
        }

        public bool IsTotalQuantityEqualApplyQuantity
        {
            get
            {
                return TotalQuantity == ApplyQuantity;
            }
        }

        public void NotifyTotalQuantityChanged()
        {
            RaisePropertyChanged(nameof(TotalQuantity));
            RaisePropertyChanged(nameof(IsTotalQuantityEqualApplyQuantity));
        }

    }



 
    public class PurchaseStorageDetailEditModel : ModelBase
    {
        public PurchaseStorageEditModel Model { get; set; }

        public Guid? Id { get; set; }


        [MaxLength(128)]
        public string? Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }

        /// <summary>
        /// 入库数量
        /// </summary>
        [Required]
        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value, Model.NotifyTotalQuantityChanged); }
        }

        //////////
        public WarehouseDto? SelectedWarehouse
        {
            get { return GetProperty(() => SelectedWarehouse); }
            set { SetProperty(() => SelectedWarehouse, value); }
        }


        [Required]
        public LocationDto? SelectedLocation
        {
            get { return GetProperty(() => SelectedLocation); }
            set { SetProperty(() => SelectedLocation, value); }
        }


        public PurchaseStorageDetailEditModel(PurchaseStorageEditModel model)
        {
            this.Model = model;
        }
    }


}
