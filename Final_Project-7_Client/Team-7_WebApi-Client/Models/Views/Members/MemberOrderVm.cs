using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Models.Views.Members
{
    public class MemberOrderVm
    {
        public MemberEntity memberId{ get; set; }
        
        [Display(Name = "訂單編號")]
        public int Id { get; set; }

        [Display(Name = "購買日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Display(Name = "訂單明細")]
        public List<OrderItemEntity> OrderItems { get; set; }

        [Display(Name ="運送狀態")]
        public OrderStatusEntity Status { get; set; }
    }
}