using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class CartEntity
	{
		public int Id { get; set; }
		public MemberEntity Member { get; set; }
        public List<CartItemEntity> CartItems { get; set; }
    }

	public static class CartEntityExtenssion
	{
		public static CartEntity ToEntity(this CartDTO dto)
		{
			return new CartEntity
			{
				Id = dto.Id,
				Member = dto.Member.ToEntity(),
				CartItems = dto.CartItems.Select(x => new CartItemEntity
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