using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public MemberEntity Member { get; set; }
        public StatusEntity OrderStatus { get; set; }
        public string Address { get; set; }
        public DateTime OrderTime { get; set; }
        public List<OrderItemEntity> OrderItemList { get; set; }
    }
}