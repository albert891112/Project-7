using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.DTOs
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public OrderDTO Order { get; set; }
        public ProductDTO Product { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public int Subtotal { get; set; }
    }
    public static class OrderItemDTOExtensions
    {
        public static OrderItemDTO ToDTO(this OrderItemEntity entity)
        {
            return new OrderItemDTO
            {
                Id = entity.Id,
                Order = entity.Order.ToDTO(),
                Product = entity.Product.ToDTO(),
                ProductName = entity.Product.Name,
                Size = entity.Size,
                Qty = entity.Qty,
                Price = entity.Price,
                Subtotal = entity.Subtotal,
            };
        }

    }
}