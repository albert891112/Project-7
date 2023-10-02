using _7_Team_WebApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
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

    public static class OrderEntityExtension
    {
        public static OrderEntity ToEntity(this OrderVM ordervm)
        {
            return new OrderEntity()
            {
                Id = ordervm.Id,
                OrderStatus = new OrderStatusEntity() { Id = ordervm.StatusID, Status = ordervm.OrderStatus  },
                PhoneNumber = ordervm.PhoneNumber,
                Address = ordervm.Address,
                Payment = new PaymentEntity() { PaymentMethod = ordervm.Payment },
                Shipping = new ShippingEntity() { ShippingMethod = ordervm.Shipping },
                Total = ordervm.Total,
                OrderTime = ordervm.OrderDate,

            };
        }
    }
}