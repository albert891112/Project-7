using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;
using static Dapper.SqlMapper;

namespace Team_7_WebApi_Client.Models.Views
{
	public class CartVM
	{
		public int Id { get; set; }
		public MemberVM Member { get; set; }

		public List<CartItemVM> CartItems { get; set; }
	}

	public static class CartVMExtenssion
	{
		public static CartVM ToVM(this CartDTO dto)
		{
			return new CartVM
			{
				Id = dto.Id,
			    Member = dto.Member.ToVM(),
				CartItems = dto.CartItems.Select(x => new CartItemVM
				{
					Id = x.Id,
					ProductName = x.ProductName,
					Qty = x.Qty,
					Price = x.Price,
					Size = x.Size,
					SubTotal = x.SubTotal,
				}).ToList()
			};
		}
	}
}