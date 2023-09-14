using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Models.DTOS
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public List<CategoryEntity> Categories { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public StockEntity Stock { get; set; }
        public bool Enable { get; set; }
    }


    public static class ProductDTOExtensions
    {
        public static ProductDTO ToDTO(this ProductEntity entity)
        {
            return new ProductDTO
            {
                Id = entity.Id,
                Categories = entity.Categories,
                Name = entity.Name,
                Price = entity.Price,
                Image = entity.Image,
                Description = entity.Description,
                Stock = entity.Stock,
                Enable = entity.Enable
            };
        }
    }
}