using _7_Team_WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace _7_Team_WebApi.Models.ViewModels
{
    public class OrderItemVM
    {
        public int Id { get; set; }
        public List<OrderItemEntity> OrderItems { get; set; }

        [Display(Name = "商品名稱")]
        public string ProductName { get; set; }

        [Display(Name = "商品價格")]
        public int ProductPrice { get; set; }

        [Display(Name = "商品數量")]
        public int ProductQuantity { get; set; }

        [Display(Name = "商品尺寸")]
        public string Size { get; set; }

        [Display(Name = "小計")]
        public int Total { get; set; }
    }
}