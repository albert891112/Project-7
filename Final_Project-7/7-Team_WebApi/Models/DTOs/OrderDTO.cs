using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace _7_Team_WebApi.Models.DTOs
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
                //Coupon = entity.Coupon.ToDTO(),
                Shipping = entity.Shipping.ToDTO(),
                Payment = entity.Payment.ToDTO(),
                OrderTime = entity.OrderTime,
                Total = entity.Total,
            };
        }

        public static OrderDTO ToDTO(this OrderVM vm)
        {
            OrderStatusDTO orderStatusDTO= GetOrderStatusDTOByStatus(vm.OrderStatus);


			return new OrderDTO() 
            { 
                Id = vm.Id,
                OrderStatus=orderStatusDTO,
                PhoneNumber=vm.PhoneNumber,
                Address = vm.Address,
                OrderTime=vm.OrderDate, 
                Total = vm.Total
            };
        }

		private static OrderStatusDTO GetOrderStatusDTOByStatus(string orderStatus)
		{
			throw new NotImplementedException();
		}
	}
}