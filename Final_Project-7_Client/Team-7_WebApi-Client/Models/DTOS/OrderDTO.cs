using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Team_7_WebApi_Client.Models.Entities.OrderEntity;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class OrderDTO
	{
		public int Id { get; set; }
		public int StatusId { get; set; }
		public string Address { get; set; }
	}

	public class OrderItemDTO
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string Size { get; set; }

		public int Qty { get; set; }
		public int Price { get; set; }
		public int Subtotal { get; set; }
	}

	public class OrderSearchDTO
	{
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}

	public class OrderItemSearchDTO
	{
		public int Id { get; set; }
		public ProductEntity Product { get; set; }
		public OrderEntity Order { get; set; }
		public int Qty { get; set; }
		public int Price { get; set; }
		public string ProductName { get; set; }
		public int Subtotal { get; set; }
		public string Size { get; set; }
	}


	public static class OrderDTOExtensions
	{
		public static OrderDTO ToDTO(this OrderEntity entity)
		{
			return new OrderDTO
			{
				Id = entity.Id,
				StatusId = entity.OrderStatus.Id,
				Address = entity.Address,
			};
		}


	}

	public static class OrderItemDTOExtensions
	{
		public static OrderItemDTO ToDTO(this OrderItemEntity entity)
		{
			return new OrderItemDTO
			{
				Id = entity.Id,
				OrderId = entity.Order.Id,
				ProductId = entity.Product.Id,
				ProductName = entity.Product.Name,
				Size = entity.Size,
				Qty = entity.Qty,
				Price = entity.Price,
				Subtotal = entity.Subtotal,
			};
		}
	}
}