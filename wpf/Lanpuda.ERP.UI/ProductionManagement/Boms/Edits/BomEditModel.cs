using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.BasicData.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.ProductionManagement.Boms.Edits
{
    public class BomEditModel : ModelBase
    {
        public Guid? Id
        {
            get { return GetProperty(() => Id); }
            set { SetProperty(() => Id, value); }
        }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Required(ErrorMessage = "产品必填")]
        [GuidNotEmpty(ErrorMessage = "产品必填")]
        public Guid ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId ,value); }
        }
      

        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
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

        public ProductSourceType ProductSourceType
        {
            get { return GetProperty(() => ProductSourceType); }
            set { SetProperty(() => ProductSourceType, value); }
        }


        public ObservableCollection<BomDetailEditModel> Details
        {
            get { return GetProperty(() => Details); }
            set { SetProperty(() => Details, value); }
        }

     

        public BomEditModel()
        {
            Details = new ObservableCollection<BomDetailEditModel>();
            IsActiveSource = new Dictionary<string, bool>();
            IsActiveSource.Add("是", true);
            IsActiveSource.Add("否", false);
        }

        public BomDetailEditModel? SelectedRow { get; set; }
        public Dictionary<string, bool> IsActiveSource { get; set; }


        

    }


    public class BomDetailEditModel :ModelBase
    {
        //public Guid BomId { get; set; }
        [GuidNotEmpty(ErrorMessage = "产品必填")]
        public Guid ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId, value); }
        }
        /// <summary>
        /// 标准用量
        /// </summary>
        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }


        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
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

        public ProductSourceType ProductSourceType
        {
            get { return GetProperty(() => ProductSourceType); }
            set { SetProperty(() => ProductSourceType, value); }
        }

        public BomDetailEditModel()
        {
            
        }

       
    }
}
