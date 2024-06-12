using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lanpuda.ERP.ProductionManagement.Mrps
{
    public class MrpNode
    {
        public string ProductName { get; set; }
        public Guid ProductId { get; set; }
        public string ProductNumber { get; set; }
        public string ProductSpec { get; set; }
        public string ProductUnitName { get; set; }

        public int LeadTime { get; set; }
        /// <summary>
        /// 当前库存
        /// </summary>
        public double OnHand { get; set; }

        public MrpLine FirstMrpLine { get; set; }
        public MrpLine CurrMrpLine { get; set; }

        public int Indegree { get; set; }
        /// <summary>
        /// 毛需求列表
        /// </summary>
        public Dictionary<DateTime,double> Orders { get; set; }

        /// <summary>
        /// 订单下达列表
        /// </summary>
        public Dictionary<DateTime,double> WorkOrders { get; set;}

        public MrpNode(Guid productId, string productName,int leadTime ,double onHand, string productNumber , string productSpec, string productUnitName)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.LeadTime = leadTime;
            this.OnHand = onHand;
            FirstMrpLine = CurrMrpLine = null;
            Orders = new Dictionary<DateTime, double>();
            WorkOrders = new Dictionary<DateTime, double>();
            this.ProductNumber = productNumber;
            this.ProductSpec = productSpec;
            this.ProductUnitName = productUnitName;
        }


        public MrpNode(Guid productId, string productName, int leadTime, double onHand , Dictionary<DateTime, double> orders, string productNumber, string productSpec, string productUnitName)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.LeadTime = leadTime;
            this.OnHand = onHand;
            FirstMrpLine = CurrMrpLine = null;
            Orders = orders;
            WorkOrders = new Dictionary<DateTime, double>();
            this.ProductNumber = productNumber;
            this.ProductSpec = productSpec;
            this.ProductUnitName = productUnitName;
        }
    }
}
