using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;

namespace _7_Team_WebApi.Models.ViewModels
{
    public class OrderVM
    {
        [Display(Name = "訂單編號")]
        public int Id { get; set; }

        [Display(Name = "會員")]
        public string Member { get; set; }

        [Display(Name = "手機")]
        public string PhoneNumber { get; set; }

        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "訂單狀態")]
        public string OrderStatus { get; set; }

        [Display(Name = "付款方式")]
        public string Payment { get; set; }

        [Display(Name = "運送方式")]
        public string Shipping { get; set; }

        [Display(Name = "總金額")]
        public int Total { get; set; }

        [Display(Name = "購買日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
    }

    public static class OrderVMExtension
    {
        public static OrderVM ToVM(this OrderDTO dto)
        {
            return new OrderVM()
            {
                Id = dto.Id,
                Member = dto.Member.Account,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                OrderStatus = dto.OrderStatus.Status,
                Payment = dto.Payment.PaymentMethod,
                Shipping = dto.Shipping.ShippingMethod,
                Total = dto.Total,
                OrderDate = dto.OrderTime
            };
        }
        public static OrderVM ToVM(this OrderEntity entity)
        {
            return new OrderVM()
            {
                Id = entity.Id,
                Member = entity.Member.Account,
                PhoneNumber = entity.PhoneNumber,
                Address = entity.Address,
                OrderStatus = entity.OrderStatus.Status,
                Payment = entity.Payment.PaymentMethod,
                Shipping = entity.Shipping.ShippingMethod,
                Total = entity.Total,
                OrderDate = entity.OrderTime
            };
        }
    }
}