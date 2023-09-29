using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Albert.Lib;
using System.Collections.Specialized;

namespace _7_Team_WebApi.Models.DTOs
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
        public StockDTO Stock { get; set; }
        public bool Enable { get; set; }
    }

    public class ProductUploadDTO
    {
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public int? GenderId { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public HttpPostedFile Image { get; set; }
        public string Description { get; set; }
        public StockUploadDTO Stock { get; set; }
        public int? Enable { get; set; }
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
                Stock = entity.Stock.ToDTO(),
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

        
        public static ProductUploadDTO ToDTO(this NameValueCollection Form , HttpPostedFile Image)
        {
            return new ProductUploadDTO
            {

                Id = Form["Id"].IntHasValue(),

                CategoryId = Form["CategoryId"].IntHasValue(),
                GenderId = Form["GenderId"].IntHasValue(),
                Name = Form["Name"],
                Price = Form["Price"].IntHasValue(),
                Description = Form["Description"],
                Enable = Form["Enable"].IntHasValue(),
                Image = Image,
                Stock = new StockUploadDTO
                {
                    Id = Form["Id"].IntHasValue(),
                    S = Form["S"].IntHasValue(),
                    M = Form["M"].IntHasValue(),
                    L = Form["L"].IntHasValue(),
                    XL = Form["XL"].IntHasValue()

                }

            };
        }


        
    }

}