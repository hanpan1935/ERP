using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.ProductionManagement.Workshops.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.UI.ProductionManagement.WorkOrders.MultipleAutoCreates
{
    public class MultipleAutoCreateModel : ModelBase
    {

        public Guid? BomDetailId
        {
            get { return GetProperty(() => BomDetailId); }
            set { SetProperty(() => BomDetailId, value); }
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



        public string MpsNumber
        {
            get { return GetProperty(() => MpsNumber); }
            set { SetProperty(() => MpsNumber, value); }
        }


        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }


        public DateTime StartDate
        {
            get { return GetProperty(() => StartDate); }
            set { SetProperty(() => StartDate, value); }
        }


        public DateTime? CompletionDate
        {
            get { return GetProperty(() => CompletionDate); }
            set { SetProperty(() => CompletionDate, value); }
        }
    }


    public class BomLooupModel : ModelBase
    {
        public Guid? ParentId
        {
            get { return GetProperty(() => ParentId); }
            set { SetProperty(() => ParentId, value); }
        }

        public Guid? Id
        {
            get { return GetProperty(() => Id); }
            set { SetProperty(() => Id, value); }
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

        public ProductSourceType ProductSourceType
        {
            get { return GetProperty(() => ProductSourceType); }
            set { SetProperty(() => ProductSourceType, value); }
        }

        public int LeadTime
        {
            get { return GetProperty(() => LeadTime); }
            set { SetProperty(() => LeadTime, value); }
        }

        public double ProductionQuantity
        {
            get { return GetProperty(() => ProductionQuantity); }
            set { SetProperty(() => ProductionQuantity, value); }
        }

        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }

        public ObservableCollection<BomLooupModel> Details { get; set; }

        public BomLooupModel()
        {
            Details = new ObservableCollection<BomLooupModel>();
        }
    }
}
