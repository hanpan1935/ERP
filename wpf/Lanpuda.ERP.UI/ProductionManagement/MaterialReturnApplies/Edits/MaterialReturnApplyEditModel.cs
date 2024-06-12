using AutoMapper.Execution;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Edits
{
    public class MaterialReturnApplyEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }
   

        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

        public MaterialReturnApplyDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        public ObservableCollection<MaterialReturnApplyDetailEditModel> Details
        {
            get { return GetProperty(() => Details); }
            set { SetProperty(() => Details, value); }
        }

        public MaterialReturnApplyEditModel()
        {
            Details = new ObservableCollection<MaterialReturnApplyDetailEditModel>();
        }
    }



    public class MaterialReturnApplyDetailEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public Guid WorkOrderOutDetailId { get; set; }

        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }

        #region 辅助字段
        public Guid ProductId { get; set; }
        public string? ProductNumber
        {
            get { return GetProperty(() => ProductNumber); }
            set { SetProperty(() => ProductNumber, value); }
        }


        public string? ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }


        public string? ProductSpec
        {
            get { return GetProperty(() => ProductSpec); }
            set { SetProperty(() => ProductSpec, value); }
        }


        public string? ProductUnitName
        {
            get { return GetProperty(() => ProductUnitName); }
            set { SetProperty(() => ProductUnitName, value); }
        }

        public string? Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }
        #endregion

        public MaterialReturnApplyDetailEditModel()
        {
        }
        
    }

}
