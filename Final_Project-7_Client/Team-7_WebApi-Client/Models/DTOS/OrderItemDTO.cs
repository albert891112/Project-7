using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class OrderItemDTO
	{
		public int Id { get; set; }
		public OrderDTO Order { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public ProductDTO Product { get; set; }
		public string ProductName { get; set; }
		public string Size { get; set; }
		public int Qty { get; set; }
		public int Price { get; set; }
		public int Subtotal { get; set; }
	}

	public static class OrderItemDTOExtensions
	{
		public static OrderItemDTO ToDTO(this OrderItemEntity entity)
		{
			return new OrderItemDTO
			{
				Id = entity.Id,
				ProductId = entity.ProductId,
				OrderId = entity.OrderId,
				Order = entity.Order.ToDTO(),
				Product = entity.Product.ToDTO(),
				ProductName = entity.Product.Name,
				Size = entity.Size,
				Qty = entity.Qty,
				Price = entity.Price,
				Subtotal = entity.Subtotal,
			};
		}

		public static OrderItemDTO ToDTO(this OrderItemVM vm)
		{
			return new OrderItemDTO
			{
				Id = vm.Id,
				ProductId = vm.ProductId,
				OrderId = vm.OrderId,
				Order = vm.Order.ToDTO(),
				Product = vm.Product.ToDTO(),
				ProductName = vm.Product.Name,
				Size = vm.Size,
				Qty = vm.Qty,
				Price = vm.Price,
				Subtotal = vm.Subtotal,
			};
		}
	}
}