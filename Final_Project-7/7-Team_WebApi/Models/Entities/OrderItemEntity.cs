using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class OrderItemEntity
    {
        public int Id { get; set; }
        public OrderEntity Order { get; set; }
        public ProductEntity Product { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public int Subtotal { get; set; }
    }
}