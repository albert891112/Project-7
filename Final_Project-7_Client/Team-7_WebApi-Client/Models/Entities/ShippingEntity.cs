using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class ShippingEntity
	{
		public int Id { get; set; }
		public string ShippingMethod { get; set; }
		public int Price { get; set; }
	}

	public static class ShippingEntityExtensions
	{
		public static ShippingEntity ToEntity(this ShippingDTO dto)
		{
			return new ShippingEntity
			{
				Id = dto.Id,
				ShippingMethod = dto.ShippingMethod,
				Price = dto.Price,
			};
		}
	}
}