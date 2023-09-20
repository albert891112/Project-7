using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Views
{
	public class ShippingVM
	{
		public int Id { get; set; }
		public string ShippingMethod { get; set; }
		public int Price { get; set; }
	}

	public static class ShippingVMExtensions
	{
		public static ShippingVM ToVM(this ShippingDTO dto)
		{
			return new ShippingVM
			{
				Id = dto.Id,
				ShippingMethod = dto.ShippingMethod,
				Price = dto.Price,
			};
		}
	}
}