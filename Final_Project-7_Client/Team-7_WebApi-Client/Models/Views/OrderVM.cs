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
    	public MemberVM Member { get; set; }
		public OrderStatusVM OrderStatus { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public CouponVM Coupon { get; set; }
		public ShippingVM Shipping { get; set; }
		public PaymentVM Payment { get; set; }
		public DateTime OrderTime { get; set; }
		public int Total { get; set; }
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
	}
}