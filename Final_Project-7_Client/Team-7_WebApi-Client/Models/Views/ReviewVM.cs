using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Views
{
    public class ReviewVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string ProductSize { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public string ReviewTime { get; set; }

        public string OrderTime { get; set; }

        public MemberDTO Member { get; set; }
    }


    public static class ReviewVMExtenssion
    {
        public static ReviewVM ToVM(this ReviewDTO dto)
        {
            return new ReviewVM
            {
                Id = dto.Id,
                ProductId = dto.ProductId,
                ProductSize = dto.ProductSize,
                Description = dto.Description,
                Rating = dto.Rating,
                ReviewTime = dto.ReviewTime.ToString("yyyy-MM-dd"),
                OrderTime = dto.OrderTime.ToString("yyyy-MM-dd"),
                Member = dto.Member,
            };
        }
    }
}