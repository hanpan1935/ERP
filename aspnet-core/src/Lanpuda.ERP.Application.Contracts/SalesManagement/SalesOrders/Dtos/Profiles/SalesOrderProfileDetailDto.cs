using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos.Profiles
{
    public class SalesOrderProfileDetailDto
    {
        public Guid SalesOrderDetailId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public double Quantity { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductNumber { get; set; }

        public string ProductUnitName { get; set; }

        public string ProductSpec { get; set; }


        public double Price { get; set; }

        public double TaxRate { get; set; }

        public string Requirement { get; set; }

        /// <summary>
        /// 当前库存数量
        /// </summary>
        public double InventoryQuantity { get; set; }



        public List<SalesOrderProfileShipmentApplyDetailDto> ShipmentApplyDetails { get; set; }

        public double ShipmentApplyDetailsTotalQuantity
        { 
            get 
            {
                return ShipmentApplyDetails.Sum(m => m.Quantity);
            } 
        }


        public List<SalesOrderProfileReturnApplyDetailDto> ReturnApplyDetails { get; set; }

        public double ReturnApplyDetailsTotalQuantity
        {
            get
            {
                return ReturnApplyDetails.Sum(m => m.Quantity);
            }
        }



        public List<SalesOrderProfileOutDetailDto> OutDetails { get; set; }
        public double OutDetailsTotalQuantity
        {
            get
            {
                return OutDetails.Sum(m=>m.Quantity);
            }
        }
        public List<SalesOrderProfileReturnDetailDto> ReturnDetails { get; set; }

        public double ReturnDetailsTotalQuantity
        {
            get
            {
                return ReturnDetails.Sum(m=>m.Quantity);    
            }
        }

        public List<SalesOrderProfileMpsDto> Mpses { get; set; }

        public double MpsesTotalQuantity
        {
            get
            {
                return Mpses.Sum(m => m.Quanity);
            }
        }


        public SalesOrderProfileDetailDto()
        {
            ReturnApplyDetails = new List<SalesOrderProfileReturnApplyDetailDto>();
            ShipmentApplyDetails = new List<SalesOrderProfileShipmentApplyDetailDto>();
            OutDetails = new List<SalesOrderProfileOutDetailDto>();
            ReturnDetails = new List<SalesOrderProfileReturnDetailDto>();
            Mpses = new List<SalesOrderProfileMpsDto>();
        }


    }
}
