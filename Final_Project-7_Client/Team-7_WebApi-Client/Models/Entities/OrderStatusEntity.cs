using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class OrderStatusEntity
	{
		public int Id { get; set; }
		public string Status { get; set; }
	}


	public static class OrderStatusEntityExtensions
	{
		public static OrderStatusEntity ToEntity(this OrderStatusDTO dto)
		{
			return new OrderStatusEntity
			{
				Id = dto.Id,
				Status = dto.Status,
			};
		}
	}
}