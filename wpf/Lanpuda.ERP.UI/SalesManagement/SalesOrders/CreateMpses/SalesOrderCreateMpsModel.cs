//using Lanpuda.Client.Mvvm;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Lanpuda.ERP.SalesManagement.SalesOrders.CreateMpses
//{
//    public class SalesOrderCreateMpsModel : ModelBase
//    {
//        public Guid ProductId { get; set; }
//        public Guid SalesOrderDetailId { get; set; }

//        public DateTime DeliveryDate
//        {
//            get { return GetProperty(() => DeliveryDate); }
//            set { SetProperty(() => DeliveryDate, value); }
//        }

//        public string ProductNumber
//        {
//            get { return GetProperty(() => ProductNumber); }
//            set { SetProperty(() => ProductNumber, value); }
//        }

//        public string ProductName
//        {
//            get { return GetProperty(() => ProductName); }
//            set { SetProperty(() => ProductName, value); }
//        }

//        public string ProductSpec
//        {
//            get { return GetProperty(() => ProductSpec); }
//            set { SetProperty(() => ProductSpec, value); }
//        }

//        public string ProductUnitName
//        {
//            get { return GetProperty(() => ProductUnitName); }
//            set { SetProperty(() => ProductUnitName, value); }
//        }

//        public DateTime StartTime
//        {
//            get { return GetProperty(() => StartTime); }
//            set { SetProperty(() => StartTime, value); }
//        }


//        public DateTime CompleteTime
//        {
//            get { return GetProperty(() => CompleteTime); }
//            set { SetProperty(() => CompleteTime, value); }
//        }

//        public double OrderQuantity
//        {
//            get { return GetProperty(() => OrderQuantity); }
//            set { SetProperty(() => OrderQuantity, value); }
//        }

//        /// <summary>
//        /// 计划数量
//        /// </summary>
//        public double Quantity
//        {
//            get { return GetProperty(() => Quantity); }
//            set { SetProperty(() => Quantity, value); }
//        }

//        public string Remark
//        {
//            get { return GetProperty(() => Remark); }
//            set { SetProperty(() => Remark, value); }
//        }

//    }
//}
