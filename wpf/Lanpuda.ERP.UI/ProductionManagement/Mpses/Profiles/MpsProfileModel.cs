using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
using Lanpuda.ERP.ProductionManagement.Mpses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.ERP.BasicData.Products;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;

namespace Lanpuda.ERP.UI.ProductionManagement.Mpses.Profiles
{

    public class MpsProfileModel : ModelBase
    {
        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public MpsType MpsType
        {
            get { return GetProperty(() => MpsType); }
            set { SetProperty(() => MpsType, value); }
        }

        public DateTime StartDate
        {
            get { return GetProperty(() => StartDate); }
            set { SetProperty(() => StartDate, value); }
        }

        public DateTime CompleteDate
        {
            get { return GetProperty(() => CompleteDate); }
            set { SetProperty(() => CompleteDate, value); }
        }

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


        public bool IsConfirmed
        {
            get { return GetProperty(() => IsConfirmed); }
            set { SetProperty(() => IsConfirmed, value); }
        }

        public string CreatorSurname
        {
            get { return GetProperty(() => CreatorSurname); }
            set { SetProperty(() => CreatorSurname, value); }
        }

        public string CreatorName
        {
            get { return GetProperty(() => CreatorName); }
            set { SetProperty(() => CreatorName, value); }
        }

        public DateTime CreationTime
        {
            get { return GetProperty(() => CreationTime); }
            set { SetProperty(() => CreationTime, value); }
        }


        public Guid? PurchaseApplyId { get; set; }
        public string PurchaseApplyNumber
        {
            get { return GetProperty(() => PurchaseApplyNumber); }
            set { SetProperty(() => PurchaseApplyNumber, value); }
        }

        public ObservableCollection<MpsDetailDto> Details
        {
            get { return GetProperty(() => Details); }
            set { SetProperty(() => Details, value); }
        }


        public ObservableCollection<MrpDetailProfileModel> MrpDetailsModels
        {
            get { return GetProperty(() => MrpDetailsModels); }
            set { SetProperty(() => MrpDetailsModels, value); }
        }

        public ObservableCollection<WorkOrderDto> WorkOrderDetails
        {
            get { return GetProperty(() => WorkOrderDetails); }
            set { SetProperty(() => WorkOrderDetails, value); }
        }

        public List<MrpDetailProductProfileModel> ProductList
        {
            get { return GetProperty(() => ProductList); }
            set { SetProperty(() => ProductList, value); }
        }

        public MpsProfileModel()
        {
            Details = new ObservableCollection<MpsDetailDto>();
            MrpDetailsModels = new ObservableCollection<MrpDetailProfileModel>();
            ProductList = new List<MrpDetailProductProfileModel>();
            WorkOrderDetails = new ObservableCollection<WorkOrderDto>();
        }
    }


    public class MrpDetailProfileModel : ModelBase
    {
        public Guid Id { get; set; }


        public Guid ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId, value); }
        }


        public MrpDetailProductProfileModel Product
        {
            get { return GetProperty(() => Product); }
            set { SetProperty(() => Product, value); }
        }

        public DateTime RequiredDate
        {
            get { return GetProperty(() => RequiredDate); }
            set { SetProperty(() => RequiredDate, value); }
        }

        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }
    }

    public class MrpDetailProductProfileModel : ModelBase
    {
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

        public ProductSourceType ProductSourceType
        {
            get { return GetProperty(() => ProductSourceType); }
            set { SetProperty(() => ProductSourceType, value); }
        }
        public int ProductLeadTime
        {
            get { return GetProperty(() => ProductLeadTime); }
            set { SetProperty(() => ProductLeadTime, value); }
        }
    }

}
