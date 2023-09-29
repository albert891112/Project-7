using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class OrderItemEntity
	{
		public int Id { get; set; }
		public OrderEntity Order { get; set; }
		public int ProductId { get; set; }
		
		public ProductEntity Product { get; set; }
		public string ProductName { get; set; }
		public int Qty { get; set; }
		public int Price { get; set; }
		public string Size { get; set; }
		public int Subtotal { get; set; }

	}

	public static class OrderItemEntityExtensions
	{
		public static OrderItemEntity ToEntity(this OrderItemDTO dto)
		{
			return new OrderItemEntity
			{
				Id = dto.Id,
				ProductId = dto.ProductId,					
				Order = dto.Order.ToEntity(),
				Product = dto.Product.ToEntity(),
				ProductName = dto.Product.Name,
				Size = dto.Size,
				Qty = dto.Qty,
				Price = dto.Price,
				Subtotal = dto.Subtotal,
			};
		}

	}
}