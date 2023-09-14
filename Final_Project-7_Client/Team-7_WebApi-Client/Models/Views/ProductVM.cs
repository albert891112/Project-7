using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Models.Views
{
    public class ProductVM
    {
        public int Id { get; set; }
        public List<CategoryVM> Categories { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public StockEntity Stock { get; set; }
        public bool Enable { get; set; }
    }


    public class ProductSearchVM
    {
        public List<CategoryVM> Categories { get; set; }
        public string Name { get; set; }
        public int HeightPrice { get; set; }
        public int LowPrice { get; set; }
    }


    public static class ProductVMExtensions
    {
        public static ProductVM ToVM(this ProductDTO dto)
        {
            return new ProductVM
            {
                Id = dto.Id,
                Categories = dto.Categories.Select(c => c.ToVM()).ToList(),
                Name = dto.Name,
                Price = dto.Price,
                Image = dto.Image,
                Description = dto.Description,
                Stock = dto.Stock,
                Enable = dto.Enable
            };
        }

        public static ProductSearchVM ToVM(this ProductSearchDTO dto)
        {
            return new ProductSearchVM
            {
                Categories = dto.Categories.Select(c => c.ToVM()).ToList(),
                Name = dto.Name,
                HeightPrice = dto.HeightPrice,
                LowPrice = dto.LowPrice
            };
        }
    }
}