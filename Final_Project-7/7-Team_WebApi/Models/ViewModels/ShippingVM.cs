using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.ViewModels
{
    public class ShippingVM
    {
        public int Id { get; set; }
        public string ShippingMethod { get; set; }
        public int Price { get; set; }
    }

    public static class ShippingVMExtenssion
    {
        public static ShippingVM ToVM(this ShippingDTO vm)
        {
            return new ShippingVM()
            {
                Id = vm.Id,
                ShippingMethod = vm.ShippingMethod,
                Price = vm.Price,
            };
        }
    }
}