using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Views
{
	public class OrderItemVM
	{
		public int Id { get; set; }
		public OrderVM Order { get; set; }
		public int ProductId { get; set; }
		public ProductVM Product { get; set; }
		public string ProductName { get; set; }
		public string Size { get; set; }

		public int Qty { get; set; }
		public int Price { get; set; }
		public int Subtotal { get; set; }
	}

	public static class OrderItemVMExtensions
	{
		public static OrderItemVM ToVM(this OrderItemDTO dto)
		{
			return new OrderItemVM
			{
				Id = dto.Id,
				Order = dto.Order.ToVM(),
				ProductId = dto.Product.Id,
				Product = dto.Product.ToVM(),
				ProductName = dto.Product.Name,
				Size = dto.Size,
				Qty = dto.Qty,
				Price = dto.Price,
				Subtotal = dto.Subtotal,
			};
		}
	}
}