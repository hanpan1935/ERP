using DevExpress.Mvvm.Native;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Edits
{
    public class MpsEditModel : ModelBase
    {
        public Guid? Id { get; set; }


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

        /// <summary>
        /// 生产产品
        /// </summary>
        public Guid ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId, value); }
        }

        [Required(ErrorMessage = "必填")]
        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        /// <summary>
        /// 计划数量
        /// </summary>
        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value); }
        }

        /// <summary>
        /// 开工时间
        /// </summary>
        public DateTime StartDate
        {
            get { return GetProperty(() => StartDate); }
            set { SetProperty(() => StartDate, value, OnDateChanged); }
        }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompleteDate
        {
            get { return GetProperty(() => CompleteDate); }
            set { SetProperty(() => CompleteDate, value, OnDateChanged); }
        }

        public string Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

   

        public ObservableCollection<MpsDetailEditModel> MpsDetails
        {
            get { return GetProperty(() => MpsDetails); }
            set { SetProperty(() => MpsDetails, value); }
        }


        public MpsDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }


        public double TotalQuantity
        {
            get { return this.MpsDetails.Sum(m=>m.Quantity); }
        }



        public MpsEditModel()
        {
            MpsDetails = new ObservableCollection<MpsDetailEditModel>();
        }


        public void NotifyTotalQuantityChanged()
        {
            RaisePropertyChanged(nameof(TotalQuantity));
        }

       

        private void OnDateChanged()
        {
            List<DateTime> dates = new List<DateTime>();

            var span = CompleteDate - StartDate;
            int dayCount = span.Days;
            for (int i = 0; i <= dayCount; i++)
            {
                dates.Add(StartDate.AddDays(i));
            }


            List<MpsDetailEditModel> newList = new List<MpsDetailEditModel>();
            foreach (var item in dates)
            {
                var detail = MpsDetails.Where(m => m.ProductionDate.Day == item.Day && m.ProductionDate.Month == item.Month && m.ProductionDate.Year == item.Year).FirstOrDefault();
                if (detail == null)
                {
                    MpsDetailEditModel detailModel = new MpsDetailEditModel(this);
                    detailModel.ProductionDate = new DateTime(item.Year, item.Month, item.Day);
                    newList.Add(detailModel);
                }
                else
                {
                    MpsDetailEditModel detailModel = new MpsDetailEditModel(this);
                    detailModel.Id = detail.Id;
                    detailModel.ProductionDate = detail.ProductionDate;
                    detailModel.Quantity = detail.Quantity;
                    detailModel.Remark = detail.Remark;
                    newList.Add(detailModel);
                }
            }

            this.MpsDetails.Clear();
            foreach (var item in newList)
            {
                this.MpsDetails.Add(item);
            }
        }
    }


    public class MpsDetailEditModel : ModelBase
    {

        public MpsEditModel MpsEditModel { get; set; }

        public Guid? Id
        {
            get { return GetProperty(() => Id); }
            set { SetProperty(() => Id, value); }
        }


        public DateTime ProductionDate
        {
            get { return GetProperty(() => ProductionDate); }
            set { SetProperty(() => ProductionDate, value); }
        }


        public double Quantity
        {
            get { return GetProperty(() => Quantity); }
            set { SetProperty(() => Quantity, value, MpsEditModel.NotifyTotalQuantityChanged); }
        }


        public string Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }


        public MpsDetailEditModel(MpsEditModel mpsEditModel)
        {
            MpsEditModel = mpsEditModel;
        }
    }
}
