using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Team_7_WebApi_Client.Models.Entities.OrderEntity;
using Team_7_WebApi_Client.Models.Entities;
using System.Net;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class OrderDTO
	{
		public int Id { get; set; }
	    public MemberDTO Member { get; set; }
		public OrderStatusDTO OrderStatus { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public CouponDTO Coupon { get; set; }
		public ShippingDTO Shipping { get; set; }
		public PaymentDTO Payment { get; set; }
		public DateTime OrderTime { get; set; }
		public int Total { get; set; }
		public List<OrderItemDTO> OrderItemList { get; set; }
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
		public static OrderDTO ToDTO(this OrderVM vm)
		{
			return new OrderDTO
			{
				Id = vm.Id,
				OrderStatus = vm.OrderStatus.ToDTO(),
				PhoneNumber = vm.PhoneNumber,
				Address = vm.Address,
				Coupon = vm.Coupon.ToDTO(),
				Shipping = vm.Shipping.ToDTO(),
				Payment = vm.Payment.ToDTO(),
				OrderTime = vm.OrderTime,
				Total = vm.Total,
			};
		}
	}	
}