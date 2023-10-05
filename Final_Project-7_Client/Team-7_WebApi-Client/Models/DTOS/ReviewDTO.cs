using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string ProductSize { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public DateTime ReviewTime { get; set; }

        public DateTime OrderTime { get; set; }

        public MemberDTO Member { get; set; }

    }

    public static class ReviewEntityExtenssion
    {
        public static ReviewDTO ToDTO(this ReviewEntity eneity)
        {
            return new ReviewDTO
            {
                Id = eneity.Id,
                ProductId = eneity.ProductId,
                ProductSize = eneity.ProductSize,
                Description = eneity.Description,
                Rating = eneity.Rating,
                ReviewTime = eneity.ReviewTime,
                OrderTime = eneity.OrderTime,
                Member = eneity.Member.ToDTO(),
            };
        }

        public static ReviewDTO ToDTO(this ReviewVM vm)
        {
            return new ReviewDTO
            {
                Id = vm.Id,
                ProductId = vm.ProductId,
                ProductSize = vm.ProductSize,
                Description = vm.Description,
                Rating = vm.Rating,
            };
        }
    }
}