using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class CartItemEntity
	{
		public int Id { get; set; }
		public int ProductId { get; set; }

        public int CartId{ get; set; }
		public ProductEntity Product { get; set; }
        public int Qty { get; set; }
		public string Size { get; set; }
	}



	public class CartItemCreateEntity
	{
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public string Size { get; set; }
        public int CartId { get; set; }

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

		public static CartItemCreateEntity ToEntity(this CartItemCreateDTO dto)
		{
            return new CartItemCreateEntity
			{
                Id = dto.Id,
                ProductId = dto.ProductId,
                Qty = dto.Qty,
                Size = dto.Size,
                CartId = dto.CartId,
            };
        }
	}
}