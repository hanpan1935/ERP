using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.ProductionManagement.MaterialApplies.Edits
{
    public class MaterialApplyEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }


        public Guid WorkOrderId
        {
            get { return GetProperty(() => WorkOrderId); }
            set { SetProperty(() => WorkOrderId, value); }
        }

        [Required(ErrorMessage ="必填")]
        public string WorkOrderNumber
        {
            get { return GetProperty(() => WorkOrderNumber); }
            set { SetProperty(() => WorkOrderNumber, value); }
        }


        public string Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

        public MaterialApplyDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        public ObservableCollection<MaterialApplyDetailEditModel> Details
        {
            get { return GetProperty(() => Details); }
            set { SetProperty(() => Details, value); }
        }

        public MaterialApplyEditModel()
        {
            Details = new ObservableCollection<MaterialApplyDetailEditModel>();
        }
    }

    public class MaterialApplyDetailEditModel : ModelBase
    {
        public Guid? Id { get; set; }

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

        public string ProductNumber
        {
            get { return GetProperty(() => ProductNumber); }
            set { SetProperty(() => ProductNumber, value); }
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

        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }


        public string Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }
   
        public double StandardQuantity
        {
            get { return GetProperty(() => StandardQuantity); }
            set { SetProperty(() => StandardQuantity, value); }
        }
     
    }
}
