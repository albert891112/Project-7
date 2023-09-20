using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
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

		public static ShippingDTO ToDTO(this ShippingVM vm)
		{
			return new ShippingDTO
			{
				Id = vm.Id,
				ShippingMethod = vm.ShippingMethod,
				Price = vm.Price,
			};
		}
	}
}