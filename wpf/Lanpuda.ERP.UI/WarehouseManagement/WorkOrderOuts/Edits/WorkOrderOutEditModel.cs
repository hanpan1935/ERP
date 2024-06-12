using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Data;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Edits
{
    public class WorkOrderOutEditModel : ModelBase
    {
        public Guid Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }
   

        public string MaterialApplyNumber
        {
            get { return GetProperty(() => MaterialApplyNumber); }
            set { SetProperty(() => MaterialApplyNumber, value); }
        }

  

        [Display(Name = "备注")]
        [MaxLength(256)]
        public string Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

        public Guid ProductId { get; set; }

      

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


        public double ApplyQuantity
        {
            get { return GetProperty(() => ApplyQuantity); }
            set { SetProperty(() => ApplyQuantity, value); }
        }


        public double TotalQuantity
        {
            get
            {
                var total = Details.Sum(d => d.Quantity);
                return total;
            }
        }

        public bool IsTotalQuantityEqualsApplyQuantity
        {
            get
            {
                if (ApplyQuantity == TotalQuantity)
                {
                    return true;
                }
                return false;
            }
        }

        public WorkOrderOutDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }


        public ObservableCollection<WorkOrderOutDetailEditModel> Details
        {
            get { return GetProperty(() => Details); }
            set { SetProperty(() => Details, value, NotifyTotalQuantityChanged); }
        }


        public WorkOrderOutEditModel()
        {
            Details = new ObservableCollection<WorkOrderOutDetailEditModel>();
        }

        public void NotifyTotalQuantityChanged()
        {
            RaisePropertyChanged(nameof(TotalQuantity));
            RaisePropertyChanged(nameof(IsTotalQuantityEqualsApplyQuantity));
        }
    }
    

    public class WorkOrderOutDetailEditModel : ModelBase
    {
        private WorkOrderOutEditModel Model;

        public Guid? Id { get; set; }
    
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


        public Guid LocationId
        {
            get { return GetProperty(() => LocationId); }
            set { SetProperty(() => LocationId, value); }
        }

        [MaxLength(128)]
        public string Batch
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

        public WorkOrderOutDetailEditModel(WorkOrderOutEditModel model)
        {
            Model = model;
        }
    }
}
