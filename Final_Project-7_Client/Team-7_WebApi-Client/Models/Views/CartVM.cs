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
		public int MemberId { get; set; }

        public IEnumerable<CartItemVM> CartItems { get; set; }

		public int Total=> CartItems.Sum(x => x.SubTotal);

		public bool AllowCheckout => CartItems.Any();
	}

	//public static class CartVMExtenssion
	//{
	//	public static CartVM ToVM(this CartDTO dto)
	//	{
	//		return new CartVM
	//		{
	//			Id = dto.Id,
	//			MemberId = dto.Member.ToVM(),
	//			CartItems = dto.CartItems.Select(x => new CartItemVM
	//			{
	//				Id = x.Id,
	//				Qty = x.Qty,

	//			}).ToList()
	//		};
	//	}
	//}
}