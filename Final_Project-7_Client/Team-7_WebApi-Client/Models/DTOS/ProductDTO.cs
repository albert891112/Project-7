using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;
using Albert.Lib;

namespace Team_7_WebApi_Client.Models.DTOS
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public CategoryDTO Category { get; set; }
        public GenderCategoryDTO Gender { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public StockEntity Stock { get; set; }
        public bool Enable { get; set; }
    }

    public class ProductSearchDTO
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public int? HightPrice { get; set; }
        public int? LowPrice { get; set; }

        public int? Gender { get; set; }
    }

    public static class ProductDTOExtenssion
    {
        public static ProductDTO ToDTO(this ProductEntity entity)
        {
            return new ProductDTO
            {
                Id = entity.Id,
                Category = entity.Category.ToDTO(),
                Gender = entity.Gender.ToDTO(),
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
                Category = vm.Category.ToDTO(),
                Name = vm.Name,
                Price = vm.Price,
                Image = vm.Image,
                Description = vm.Description,
                Stock = vm.Stock,
                Enable = vm.Enable
            };
        }


        public static ProductSearchDTO ToDTO(this ProductSearchVM vm)
        {
            return new ProductSearchDTO
            {
                CategoryId = vm.CategoryId.IntHasValue(),
                Name = vm.Name,
                HightPrice = vm.HightPrice.IntHasValue(),
                LowPrice = vm.LowPrice.IntHasValue(),
                Gender = vm.Gender.IntHasValue()
            };
        }
    }


    
}