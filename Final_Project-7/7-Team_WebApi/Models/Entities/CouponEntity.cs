using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class CouponEntity
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public string CouponName { get; set; }
        public DiscountTypeEntity Discount { get; set; }
        public decimal DiscountValue { get; set; }
        public string CouponDescription { get; set; }
        public DateTime ExpiratinDate { get; set; }
        public int UsageCount { get; set; }
        public bool Enable { get; set; }
        public string Image { get; set; }
    }

    public class DiscountTypeEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}