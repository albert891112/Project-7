using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class PaymentEntity
	{
		public int Id { get; set; }
		public string PaymentMethod { get; set; }
	}

	public static class PaymentEntityExtensions
	{
		public static PaymentEntity ToEntity(this PaymentDTO dto)
		{
			return new PaymentEntity
			{
				Id = dto.Id,
				PaymentMethod = dto.PaymentMethod,
			};
		}
	}
}