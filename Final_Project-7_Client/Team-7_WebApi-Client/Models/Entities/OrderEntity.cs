using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class OrderEntity
	{		
			public int Id { get; set; }
			public MemberEntity Member { get; set; }
			public StatusEntity OrderStatus { get; set; }
			public string Address { get; set; }
			public DateTime OrderTime { get; set; }
			public List<OrderItemEntity> OrderItemList { get; set; }
		

		public class OrderItemEntity
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

		public class OrderSearchEntity
		{
			public DateTime StartTime { get; set; }
			public DateTime EndTime { get; set; }
		}

		public class OrderItemSearchEntity
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

		public static class OrederEntityExtensions
		{
			public static OrderSearchEntity ToEntity(this OrderSearchDTO dto)
			{
				return new OrderSearchEntity
				{
					StartTime = dto.StartTime,
					EndTime = dto.EndTime,
				};
			}
		}

		public static class OrederItemEntityExtensions
		{
			public static OrderItemSearchEntity ToEntity(this OrderItemSearchDTO dto)
			{
				return new OrderItemSearchEntity
				{
					Product = dto.Product,
					Order = dto.Order,
					Qty = dto.Qty,
					Price = dto.Price,
					ProductName = dto.ProductName,
					Subtotal = dto.Subtotal,
					Size = dto.Size,
				};
			}
		}
	}
}