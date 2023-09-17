using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public CategoryEntity Category { get; set; }
        public GenderCategoryEntity Gender { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public StockEntity Stock { get; set; }
        public bool Enable { get; set; }
    }

    public class ProductSearchEntity
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }

        public int? Gender { get; set; }
        public int? HightPrice { get; set; }
        public int? LowPrice { get; set; }

    }

    public static class ProductEntityExtenssion
    {
        public static ProductEntity ToEntity(this ProductDTO dto)
        {
            return new ProductEntity
            {
                Id = dto.Id,
                Category = dto.Category.ToEntity(),
                Name = dto.Name,
                Price = dto.Price,
                Image = dto.Image,
                Description = dto.Description,
                Stock = dto.Stock,
                Enable = dto.Enable
            };
        }


        public static ProductSearchEntity ToEntity(this ProductSearchDTO dto)
        {
            return new ProductSearchEntity
            {
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                HightPrice = dto.HightPrice,
                LowPrice = dto.LowPrice,
                Gender = dto.Gender
            } ;
        }
    }

   
    
}