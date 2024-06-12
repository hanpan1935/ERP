using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.Workshops.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders.MultipleCreates
{
    public class MultipleCreateModel : ModelBase
    {
        public Guid MpsId
        {
            get { return GetProperty(() => MpsId); }
            set { SetProperty(() => MpsId, value); }
        }
       

       

        [Required(ErrorMessage ="必填")]
        public string MpsNumber
        {
            get { return GetProperty(() => MpsNumber); }
            set { SetProperty(() => MpsNumber, value); }
        }

        public ObservableCollection<MultipleCreateDetailModel> Details
        {
            get { return GetProperty(() => Details); }
            set { SetProperty(() => Details, value); }
        }

        public MultipleCreateDetailModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        public MultipleCreateModel()
        {
            Details = new ObservableCollection<MultipleCreateDetailModel>();
            WorkshopSource = new ObservableCollection<WorkshopDto>();
        }

        public ObservableCollection<WorkshopDto> WorkshopSource { get; set; }
    }


    public class MultipleCreateDetailModel : ModelBase
    {

        public MultipleCreateModel Model { get; set; }

        public WorkshopDto? SelectedWorkshop
        {
            get { return GetProperty(() => SelectedWorkshop); }
            set { SetProperty(() => SelectedWorkshop, value); }
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
            set { SetProperty(() => StartDate, value, OnStartDateChanged); }
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

        public int? LeadTime
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }
      

        private void OnStartDateChanged()
        {
            if (LeadTime != null)
            {
                CompletionDate = StartDate.AddDays((double)LeadTime);
            }
        }

        public MultipleCreateDetailModel(MultipleCreateModel model)
        {
            Model = model;
        }
    }
}
