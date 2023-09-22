using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class CartItemEntity
	{
		public int Id { get; set; }
		public int ProductId { get; set; }

        public CartEntity Cart{ get; set; }
		public ProductEntity Product { get; set; }
        public int Qty { get; set; }
		public string Size { get; set; }
	}

	public static class CartItemEntityExtenssion
	{
		public static CartItemEntity ToEntity(this CartItemDTO dto)
		{
			return new CartItemEntity
			{
				Id = dto.Id,
				Qty = dto.Qty,
				Size = dto.Size,

			};
		}
	}
}