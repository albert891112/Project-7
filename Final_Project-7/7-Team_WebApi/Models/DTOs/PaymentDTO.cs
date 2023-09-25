using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
    }

    public static class PaymentDTOExtensions
    {
        public static PaymentDTO ToDTO(this PaymentEntity entity)
        {
            return new PaymentDTO
            {
                Id = entity.Id,
                PaymentMethod = entity.PaymentMethod,
            };
        }

    }
}