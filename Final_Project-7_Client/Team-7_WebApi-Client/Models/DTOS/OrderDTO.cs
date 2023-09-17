using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Team_7_WebApi_Client.Models.Entities.OrderEntity;
using Team_7_WebApi_Client.Models.Entities;
using System.Net;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class OrderDTO
	{
		public int Id { get; set; }
		//todo public MemberDTO Member { get; set; }
		public StatusDTO OrderStatus { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public CouponDTO Coupon { get; set; }
		public ShippingDTO Shipping { get; set; }
		public PaymentDTO Payment { get; set; }
		public DateTime OrderTime { get; set; }
		public int Total { get; set; }
		//public List<OrderItemDTO> OrderItemList { get; set; }
	}

	public class OrderItemDTO
	{
		public int Id { get; set; }
		public OrderDTO Order { get; set; }
		public ProductDTO Product { get; set; }
		public string ProductName { get; set; }
		public string Size { get; set; }
		public int Qty { get; set; }
		public int Price { get; set; }
		public int Subtotal { get; set; }
	}

	//public class OrderSearchDTO
	//{
	//	public DateTime StartTime { get; set; }
	//	public DateTime EndTime { get; set; }
	//}

	//public class OrderItemSearchDTO
	//{
	//	public int Id { get; set; }
	//	public ProductEntity Product { get; set; }
	//	public OrderEntity Order { get; set; }
	//	public int Qty { get; set; }
	//	public int Price { get; set; }
	//	public string ProductName { get; set; }
	//	public int Subtotal { get; set; }
	//	public string Size { get; set; }
	//}

	public class StatusDTO
	{
		public int Id { get; set; }
		public string Status { get; set; }
	}



	public class ShippingDTO
	{
		public int Id { get; set; }
		public string ShippingMethod { get; set; }
		public int Price { get; set; }
	}

	public class PaymentDTO
	{
		public int Id { get; set; }
		public string PaymentMethod { get; set; }
	}


	public static class OrderDTOExtensions
	{
		public static OrderDTO ToDTO(this OrderEntity entity)
		{
			return new OrderDTO
			{
				Id = entity.Id,
				OrderStatus = entity.OrderStatus.ToDTO(),
				PhoneNumber = entity.PhoneNumber,
				Address = entity.Address,
				Coupon = entity.Coupon.ToDTO(),
				Shipping = entity.Shipping.ToDTO(),
				Payment = entity.Payment.ToDTO(),
				OrderTime = entity.OrderTime,
				Total = entity.Total,
			};
		}

		public static StatusDTO ToDTO(this StatusEntity entity)
		{
			return new StatusDTO
			{
				Id = entity.Id,
				Status = entity.Status,				
			};
		}

		public static ShippingDTO ToDTO(this ShippingEntity entity)
		{
			return new ShippingDTO
			{
				Id = entity.Id,
				ShippingMethod = entity.ShippingMethod,
				Price = entity.Price,
			};
		}


		public static PaymentDTO ToDTO(this PaymentEntity entity)
		{
			return new PaymentDTO
			{
				Id = entity.Id,
				PaymentMethod = entity.PaymentMethod,
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
				Order = entity.Order.ToDTO(),
				Product = entity.Product.ToDTO(),
				ProductName = entity.Product.Name,
				Size = entity.Size,
				Qty = entity.Qty,
				Price = entity.Price,
				Subtotal = entity.Subtotal,
			};
		}
	}
}