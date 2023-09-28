using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class OrderStatusEntity
    {
        public int Id { get; set; }
        public string Status { get; set; }

		public string Text { get; set; }
		public string Value { get; set; }
	}

    public static class OrderStatusEntityExtension
    {
        public static OrderStatusEntity ToEntity(this OrderStatusDTO dto) 
        {
            return new OrderStatusEntity() 
            { Id = dto.Id, Status = dto.Status };
        }
    }
}