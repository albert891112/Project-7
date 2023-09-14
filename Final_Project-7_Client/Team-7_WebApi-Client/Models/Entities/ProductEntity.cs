using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
    public class ProductEntity
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

    public class ProductSearchEntity
    {
        public List<CategoryEntity> Categiries { get; set; }
        public string Name { get; set; }
        public int HeightPrice { get; set; }
        public int LowPrice { get; set; }
    }

    public static class ProductEntityExtensions
    {

        public static ProductSearchEntity ToEntity(this ProductSearchDTO dto)
        {
            return new ProductSearchEntity
            {
                Categiries = dto.Categories.Select(c => c.ToEntity()).ToList(),
                Name = dto.Name,
                HeightPrice = dto.HeightPrice,
                LowPrice = dto.LowPrice
            };
        }
    }
    
}