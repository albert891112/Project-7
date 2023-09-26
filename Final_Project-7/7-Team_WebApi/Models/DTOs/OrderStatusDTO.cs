using _7_Team_WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.DTOs
{
    public class OrderStatusDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }

    public static class OrderStatusDTOExtensions
    {
        public static OrderStatusDTO ToDTO(this OrderStatusEntity entity)
        {
            return new OrderStatusDTO
            {
                Id = entity.Id,
                Status = entity.Status,
            };
        }

    }
}