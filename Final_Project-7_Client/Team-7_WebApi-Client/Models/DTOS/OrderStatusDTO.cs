using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
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

		public static OrderStatusDTO ToDTO(this OrderStatusVM vm)
		{
			return new OrderStatusDTO
			{
				Id = vm.Id,
				Status = vm.Status,
			};
		}
	}
}