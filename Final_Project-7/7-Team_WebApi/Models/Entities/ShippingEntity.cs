using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class ShippingEntity
    {
        public int Id { get; set; }
        public string ShippingMethod { get; set; }
        public int Price { get; set; }
    }
}