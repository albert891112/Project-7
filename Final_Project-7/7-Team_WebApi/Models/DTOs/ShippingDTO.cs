using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.DTOs
{
    public class ShippingDTO
    {
        public int Id { get; set; }
        public string ShippingMethod { get; set; }
        public int Price { get; set; }
    }

    public static class ShippingDTOExtensions
    {
        public static ShippingDTO ToDTO(this ShippingEntity entity)
        {
            return new ShippingDTO
            {
                Id = entity.Id,
                ShippingMethod = entity.ShippingMethod,
                Price = entity.Price,
            };
        }

    }
}