using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.EFModels;

namespace Team_7_WebApi_Client.Models.Views
{
	public class OrderStatusVM
	{
		public int Id { get; set; }
		public string Status { get; set; }

	}

	public static class OrderStatusVMExtensions
	{

		public static OrderStatusVM ToVM(this OrderStatusDTO dto)
		{
			return new OrderStatusVM
			{
				Id = dto.Id,
				Status = dto.Status,
			};
		}
	}
}