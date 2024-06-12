using HandyControl.Collections;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.InventoryChecks.Edits
{
    public class InventoryCheckEditModel : ModelBase
    {
        public Guid? Id { get; set; }
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public DateTime CheckDate
        {
            get { return GetProperty(() => CheckDate); }
            set { SetProperty(() => CheckDate, value); }
        }
    

        public string Discription
        {
            get { return GetProperty(() => Discription); }
            set { SetProperty(() => Discription, value); }
        }

        public Guid WarehouseId
        {
            get { return GetProperty(() => WarehouseId); }
            set { SetProperty(() => WarehouseId, value, async () => { await OnWarehouseIdChangedAsync(); }); }
        }


        public InventoryCheckDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        public ManualObservableCollection<InventoryCheckDetailEditModel> Details { get; set; }

        public Func<Guid,Task>? OnWarehouseChangedAsync;


        public InventoryCheckEditModel()
        {
            Details = new ManualObservableCollection<InventoryCheckDetailEditModel>();
            WarehouseSource = new ObservableCollection<WarehouseDto>();
        }

        public ObservableCollection<WarehouseDto> WarehouseSource
        {
            get { return GetProperty(() => WarehouseSource); }
            set { SetProperty(() => WarehouseSource, value); }
        }

        private async Task OnWarehouseIdChangedAsync()
        {
            if (OnWarehouseChangedAsync != null)
            {
                await OnWarehouseChangedAsync(this.WarehouseId);
            }
        }
    }

    public class InventoryCheckDetailEditModel : ModelBase
    {
        public Guid? Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }

        public string LocationName
        {
            get { return GetProperty(() => LocationName); }
            set { SetProperty(() => LocationName, value); }
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


        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }

        public double InventoryQuantity
        {
            get { return GetProperty(() => InventoryQuantity); }
            set { SetProperty(() => InventoryQuantity, value); }
        }

        /// <summary>
        /// 盘盈盘亏
        /// </summary>
        public InventoryCheckDetailType CheckType
        {
            get { return GetProperty(() => CheckType); }
            set { SetProperty(() => CheckType, value); }
        }

        /// <summary>
        /// 盈亏数量
        /// </summary>
        public double CheckQuantity
        {
            get { return GetProperty(() => CheckQuantity); }
            set { SetProperty(() => CheckQuantity, value); }
        }


        /// <summary>
        /// 实际库存
        /// </summary>
        public double RealQuantity
        {
            get { return GetProperty(() => RealQuantity); }
            set { SetProperty(() => RealQuantity, value, OnRealQuantityChanged); }
        }

        private void OnRealQuantityChanged()
        {
            if (this.RealQuantity == this.InventoryQuantity)
            {
                this.CheckType = InventoryCheckDetailType.None;
                this.CheckQuantity = 0;
            }
            else
            {
                var quantity = this.InventoryQuantity - RealQuantity;
                if (quantity < 0)
                {
                    this.CheckQuantity = RealQuantity - InventoryQuantity;
                    this.CheckType = InventoryCheckDetailType.Add;
                }
                else
                {
                    this.CheckQuantity = InventoryQuantity - RealQuantity;
                    this.CheckType = InventoryCheckDetailType.Reduce;
                }
            }
        }
    }
}
