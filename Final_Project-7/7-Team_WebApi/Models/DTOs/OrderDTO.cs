using _7_Team_WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public MemberEntity Member { get; set; }
        public StatusDTO OrderStatus { get; set; }
        public string Address { get; set; }
        public DateTime OrderTime { get; set; }
        public List<OrderItemDTO> OrderItemList { get; set; }
    }
}