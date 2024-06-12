using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Edits
{
    public class PurchaseReturnApplyEditModel : ModelBase
    {
        public Guid? Id { get; set; }


        [MaxLength(128)]
        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        //[GuidNotEmpty(ErrorMessage ="必填")]
        //public Guid SupplierId
        //{
        //    get { return GetProperty(() => SupplierId); }
        //    set { SetProperty(() => SupplierId, value); }
        //}

        [Required(ErrorMessage = "必填")]
        public SupplierDto? SelectedSupplier
        {
            get { return GetProperty(() => SelectedSupplier); }
            set { SetProperty(() => SelectedSupplier, value); }
        }


        /// <summary>
        /// 退货原因
        /// </summary>
        public PurchaseReturnReason ReturnReason
        {
            get { return GetValue<PurchaseReturnReason>(); }
            set { SetValue(value); }
        }

        [MaxLength(256)]
        [Required(ErrorMessage = "问题描述必填")]
        public string Description
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        [MaxLength(256)]
        public string? Remark
        {
            get { return GetValue<string?>(); }
            set { SetValue(value); }
        }


        public PurchaseReturnApplyDetailEditModel? SelectedRow
        {
            get { return GetValue<PurchaseReturnApplyDetailEditModel?>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<PurchaseReturnApplyDetailEditModel> Details
        {
            get { return GetProperty(() => Details); }
            set { SetProperty(() => Details, value); }
        }
        public PurchaseReturnApplyEditModel()
        {
            Details = new ObservableCollection<PurchaseReturnApplyDetailEditModel>();
        }
    }


    public class PurchaseReturnApplyDetailEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public string PurchaseStorageNumber
        {
            get { return GetProperty(() => PurchaseStorageNumber); }
            set { SetProperty(() => PurchaseStorageNumber, value); }
        }

        public string Batch
        {
            get { return GetProperty(() => Batch); }
            set { SetProperty(() => Batch, value); }
        }

        [GuidNotEmpty(ErrorMessage = "必填")]
        public Guid PurchaseStorageDetailId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }


        public double StorageQuantity
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }
        /// <summary>
        /// 退货数量
        /// </summary>
        [Required]
        public double Quantity
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }



        public string Remark
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        #region 辅助字段

        public string ProductNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ProductName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
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




        #endregion


    }


}
