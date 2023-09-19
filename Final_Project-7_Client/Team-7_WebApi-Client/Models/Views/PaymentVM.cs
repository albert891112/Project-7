using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Views
{
	public class PaymentVM
	{
		public int Id { get; set; }
		public string PaymentMethod { get; set; }
	}

	public static class PaymentVMExtensions
	{
		public static PaymentVM ToVM(this PaymentDTO dto)
		{
			return new PaymentVM
			{
				Id = dto.Id,
				PaymentMethod = dto.PaymentMethod,
			};
		}
	}
}