﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class OrderEntity
	{
		public int Id { get; set; }
		public MemberEntity Member { get; set; }
		public OrderStatusEntity OrderStatus { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public CouponEntity Coupon { get; set; }
		public ShippingEntity Shipping { get; set; }
		public PaymentEntity Payment { get; set; }
		public DateTime OrderTime { get; set; }
		public int Total { get; set; }
		public List<OrderItemEntity> OrderItemList { get; set; }
	}


	public static class OrderEntityExtensions
	{
		public static OrderEntity ToEntity(this OrderDTO dto)
		{
			return new OrderEntity
			{
				Id = dto.Id,
				OrderStatus = dto.OrderStatus.ToEntity(),
				PhoneNumber = dto.PhoneNumber,
				Address = dto.Address,
				Coupon = dto.Coupon.ToEntity(),
				Shipping = dto.Shipping.ToEntity(),
				Payment = dto.Payment.ToEntity(),
				OrderTime = dto.OrderTime,
				Total = dto.Total,
			};
		}
	}
}