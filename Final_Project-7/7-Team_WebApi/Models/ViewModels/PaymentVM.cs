using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.ViewModels
{
    public class PaymentVM
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
    }

    public static class PaymentVMExtenssion
    {
        public static PaymentVM ToVM(this PaymentDTO dto)
        {
            return new PaymentVM()
            {
                Id = dto.Id,
                PaymentMethod = dto.PaymentMethod,
            };
        }
    }
}