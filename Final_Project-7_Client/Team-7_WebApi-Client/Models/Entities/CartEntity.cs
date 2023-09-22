using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.EFModels;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class CartEntity
	{
		public int Id { get; set; }
		public int MemberId { get; set; }
        public List<CartItemEntity> CartItems { get; set; }
    }

	public static class CartEntityExtenssion
	{
		public static CartEntity ToEntity(this CartDTO dto)
		{
			return new CartEntity
			{
				Id = dto.Id,
				MemberId = dto.MemberId,
				CartItems = dto.CartItems.Select(x => new CartItemEntity
				{
					Id = x.Id,
					Qty = x.Qty,
					Size = x.Size,


				}).ToList()
			};
		}


		public static CartEntity ToEnity(this Cart cart)
		{
			return new CartEntity
			{
				Id = cart.Id,
				MemberId = cart.MemberId,
				CartItems = cart.CartItems.Select(x => new CartItemEntity
				{
					Id = x.Id,
					ProductId = x.ProductId,
					Qty = x.Qty,
					Size = x.Size,

				}).ToList()
			};
		}
	}
}