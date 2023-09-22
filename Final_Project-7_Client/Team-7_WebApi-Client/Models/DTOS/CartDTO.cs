using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class CartDTO
	{
		public int Id { get; set; }
	    public MemberDTO Member { get; set; }

		public List<CartItemDTO> CartItems { get; set; }
	}
	
	public static class CartDTOExtenssion
	{
		public static CartDTO ToDTO(this CartEntity entity)
		{
			return new CartDTO
			{
				Id = entity.Id,
				Member = entity.Member.ToDTO(),
				CartItems = entity.CartItems.Select(x => new CartItemDTO
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


		//public static CartDTO ToDTO(this CartVM vm)
		//{
		//	return new CartDTO
		//	{
		//		Id = vm.Id,
		//		Member = vm.Member.ToDTO(),
		//		CartItems = vm.CartItems.Select(x => new CartItemDTO
		//		{
		//			Id = x.Id,
		//			ProductName = x.ProductName,
		//			Qty = x.Qty,
		//			Price = x.Price,
		//			Size = x.Size,
		//			SubTotal = x.SubTotal,
		//		}).ToList()
		//	};
		//}
	}


	
}