using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.ProductionManagement.Workshops.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.Edits
{
    public class WorkOrderEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public WorkshopDto? SelectedWorkshop
        {
            get { return GetProperty(() => SelectedWorkshop); }
            set { SetProperty(() => SelectedWorkshop, value); }
        }

        public ObservableCollection<WorkshopDto> WorkshopSource { get; set; }

        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }


        public Guid MpsId
        {
            get { return GetProperty(() => MpsId); }
            set { SetProperty(() => MpsId, value); }
        }


        public string? MpsNumber
        {
            get { return GetProperty(() => MpsNumber); }
            set { SetProperty(() => MpsNumber, value , ()=> { this.ProductId = Guid.Empty; }); }
        }


        public Guid ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId, value); }
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

        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

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


        public WorkOrderEditModel()
        {
            WorkshopSource = new ObservableCollection<WorkshopDto>();
        }
    }

    public class WorkOrderMaterialModel : ModelBase
    {
        public Guid? Id { get; set; }

        public Guid ProductId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }

        public double Quantity
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

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


        public ProductSourceType ProductSourceType
        {
            get { return GetValue<ProductSourceType>(); }
            set { SetValue(value); }
        }
    }
}
