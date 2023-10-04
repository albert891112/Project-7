using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;

namespace Team_7_WebApi_Client.Models.Entities
{
    public class ReviewEntity
    {
        public int Id { get; set; }

        public string ProductSize { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public DateTime ReviewTime { get; set; }

        public DateTime OrderTime { get; set; }

        public MemberEntity Member { get; set; }

        public ProductEntity Product { get; set; }
    }


    public static class ReviewEntityExtenssion
    {
        public static ReviewEntity ToEntity(this Review model)
        {
            return new ReviewEntity
            {
                Id = model.Id,
                ProductSize = model.ProductSize,
                Description = model.Description,
                Rating = model.Rating,
                ReviewTime = model.ReviewTime,
                OrderTime = model.OrderTime,
                Member = model.Member.ToEntity(),
                Product = model.Product.ToEntity()
            };
        }
    }
}