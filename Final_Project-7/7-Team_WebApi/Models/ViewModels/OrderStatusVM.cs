using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.ViewModels
{
    public class OrderStatusVM
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }

    public static class OrderStatusExtenssion
    {
        public static OrderStatusVM ToVM(this OrderStatusDTO dto)
        {
            return new OrderStatusVM()
            {
                Id = dto.Id,
                Status = dto.Status,
            };
        }
    } 
    
}