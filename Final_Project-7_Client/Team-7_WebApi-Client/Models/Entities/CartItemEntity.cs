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
		public OrderEntity Order { get; set; }
		public ProductEntity Product { get; set; }
		public string ProductName { get; set; }
		public int Qty { get; set; }
		public int Price { get; set; }
		public string Size { get; set; }
		public int SubTotal { get; set; }
	}

	public static class CartItemEntityExtenssion
	{
		public static CartItemEntity ToEntity(this CartItemDTO dto)
		{
			return new CartItemEntity
			{
				Id = dto.Id,
				Order = dto.Order.ToEntity(),
				Product = dto.Product.ToEntity(),
				ProductName = dto.ProductName,
				Qty = dto.Qty,
				Price = dto.Price,
				Size = dto.Size,
				SubTotal = dto.SubTotal,
			};
		}
	}
}