using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public StockEntity Stock { get; set; }
        public bool Enable { get; set; }
    }

    public class ProductSearchDTO
    {
        public List<CategoryDTO> Categories { get; set; }
        public string Name { get; set; }
        public int HeightPrice { get; set; }
        public int LowPrice { get; set; }
    }


    public static class ProductDTOExtensions
    {
        public static ProductDTO ToDTO(this ProductEntity entity)
        {
            return new ProductDTO
            {
                Id = entity.Id,
                Categories = entity.Categories.Select(c => c.ToDTO()).ToList(),
                Name = entity.Name,
                Price = entity.Price,
                Image = entity.Image,
                Description = entity.Description,
                Stock = entity.Stock,
                Enable = entity.Enable
            };
        }

        public static ProductSearchDTO ToDTO(this ProductSearchVM vm)
        {
            return new ProductSearchDTO
            {
                Categories = vm.Categories.Select(c => c.ToDTO()).ToList(),
                Name = vm.Name,
                HeightPrice = vm.HeightPrice,
                LowPrice = vm.LowPrice
            };
        }
    }
}