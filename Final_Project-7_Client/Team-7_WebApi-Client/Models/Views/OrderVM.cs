using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;
using static Dapper.SqlMapper;

namespace Team_7_WebApi_Client.Models.Views
{
	public class OrderVM
	{
		public int Id { get; set; }
		//todo public MemberDTO Member { get; set; }
		public StatusVM OrderStatus { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public CouponVM Coupon { get; set; }
		public ShippingVM Shipping { get; set; }
		public PaymentVM Payment { get; set; }
		public DateTime OrderTime { get; set; }
		public int Total { get; set; }
	}

	public class OrderItemVM
	{
		public int Id { get; set; }
		public OrderVM Order { get; set; }
		public ProductVM Product { get; set; }
		public string ProductName { get; set; }
		public string Size { get; set; }

		public int Qty { get; set; }
		public int Price { get; set; }
		public int Subtotal { get; set; }
	}


	public class StatusVM
	{
		public int Id { get; set; }
		public string Status { get; set; }
	}



	public class ShippingVM
	{
		public int Id { get; set; }
		public string ShippingMethod { get; set; }
		public int Price { get; set; }
	}

	public class PaymentVM
	{
		public int Id { get; set; }
		public string PaymentMethod { get; set; }
	}

	public static class OrderVMExtensions
	{
		public static OrderVM ToVM(this OrderDTO dto)
		{
			return new OrderVM
			{
				Id = dto.Id,
				OrderStatus = dto.OrderStatus.ToVM(),
				PhoneNumber = dto.PhoneNumber,
				Address = dto.Address,
				Coupon = dto.Coupon.ToVM(),
				Shipping = dto.Shipping.ToVM(),
				Payment = dto.Payment.ToVM(),
				OrderTime = dto.OrderTime,
				Total = dto.Total,
			};
		}

		public static StatusVM ToVM(this StatusDTO dto)
		{
			return new StatusVM
			{
				Id = dto.Id,
				Status = dto.Status,
			};
		}

		public static ShippingVM ToVM(this ShippingDTO dto)
		{
			return new ShippingVM
			{
				Id = dto.Id,
				ShippingMethod = dto.ShippingMethod,
				Price = dto.Price,
			};
		}


		public static PaymentVM ToVM(this PaymentDTO dto)
		{
			return new PaymentVM
			{
				Id = dto.Id,
				PaymentMethod = dto.PaymentMethod,
			};
		}
	}

	public static class OrderItemVMExtensions
	{
		public static OrderItemVM ToDVM(this OrderItemDTO dto)
		{
			return new OrderItemVM
			{
				Id = dto.Id,
				Order = dto.Order.ToVM(),
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