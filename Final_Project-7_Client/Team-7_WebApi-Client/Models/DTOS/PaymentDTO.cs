using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class PaymentDTO
	{
		public int Id { get; set; }
		public string PaymentMethod { get; set; }
	}

	public static class PaymentDTOExtensions
	{
		public static PaymentDTO ToDTO(this PaymentEntity entity)
		{
			return new PaymentDTO
			{
				Id = entity.Id,
				PaymentMethod = entity.PaymentMethod,
			};
		}

		public static PaymentDTO ToDTO(this PaymentVM vm)
		{
			return new PaymentDTO
			{
				Id = vm.Id,
				PaymentMethod = vm.PaymentMethod,
			};
		}
	}

}