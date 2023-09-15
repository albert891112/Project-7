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
        public CategoryDTO Categories { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public StockEntity Stock { get; set; }
        public bool Enable { get; set; }
    }

    public class ProductSearchDTO
    {
        public CategoryDTO Categories { get; set; }
        public string Name { get; set; }
        public int HeightPrice { get; set; }
        public int LowPrice { get; set; }
    }

    public static class ProductDTOExtenssion
    {
        public static ProductDTO ToDTO(this ProductEntity entity)
        {
            return new ProductDTO
            {
                Id = entity.Id,
                Categories = entity.Categories.ToDTO(),
                Name = entity.Name,
                Price = entity.Price,
                Image = entity.Image,
                Description = entity.Description,
                Stock = entity.Stock,
                Enable = entity.Enable
            };
        }

        public static ProductDTO ToDTO(this ProductVM vm)
        {
            return new ProductDTO
            {
                Id = vm.Id,
                Categories = vm.Categories.ToDTO(),
                Name = vm.Name,
                Price = vm.Price,
                Image = vm.Image,
                Description = vm.Description,
                Stock = vm.Stock,
                Enable = vm.Enable
            };
        }

        public static ProductSearchDTO ToDTO(this ProductSearchEntity entity)
        {
            return new ProductSearchDTO
            {
                Categories = entity.Categories.ToDTO(),
                Name = entity.Name,
                HeightPrice = entity.HeightPrice,
                LowPrice = entity.LowPrice
            };
        }

        public static ProductSearchDTO ToDTO(this ProductSearchVM vm)
        {
            return new ProductSearchDTO
            {
                Categories = vm.Categories.ToDTO(),
                Name = vm.Name,
                HeightPrice = vm.HeightPrice,
                LowPrice = vm.LowPrice
            };
        }
    }


    
}