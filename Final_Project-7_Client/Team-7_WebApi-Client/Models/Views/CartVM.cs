using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Models.Views
{
	public class CartVM
	{
		public int Id { get; set; }
		public int MemberId { get; set; }

        public List<CartItemVM> CartItems { get; set; }
		
        public int Total { get; set; }

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