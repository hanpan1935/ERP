using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Dtos.Profiles
{
    [Serializable]
    public class SalesOrderProfileDto : EntityDto<Guid>
    {
        /// <summary>
        /// 订单编号*
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Number { get; set; }


        /// <summary>
        /// 客户Id*
        /// </summary>
        public Guid CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerShortName { get; set; }


        /// <summary>
        /// 要求交期*
        /// </summary>
        public DateTime? RequiredDate { get; set; }

        /// <summary>
        /// 承诺交期*
        /// </summary>
        public DateTime? PromisedDate { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public SalesOrderType OrderType { get; set; }

        /// <summary>
        /// 订单说明
        /// </summary>
        [MaxLength(256, ErrorMessage = "最长256个字符")]
        public string Description { get; set; }


        /// <summary>
        /// 关闭状态
        /// </summary>
        public SalesOrderCloseStatus CloseStatus { get; set; }


        public string CreatorSurname { get; set; }
        public string CreatorName { get; set; }

        /// <summary>
        /// 是否确认(暂存,提交)
        /// </summary>
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmeTime { get; set; }
        public Guid? ConfirmeUserId { get; set; }
        public string ConfirmeUserSurname { get; set; }
        public string ConfirmeUserName { get; set; }


        /// <summary>
        /// 订单明细
        /// </summary>
        public List<SalesOrderProfileDetailDto> Details { get; set; }



        public double Amount 
        {
            get
            {
                double reuslt = 0;
                foreach (var item in Details)
                {
                    var total = item.Price * item.Quantity;
                    reuslt += total;
                }
                return reuslt;
            }
         }



        public SalesOrderProfileDto()
        {
            Details = new List<SalesOrderProfileDetailDto>();
        }
    }
}
