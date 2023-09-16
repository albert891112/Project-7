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
        public CategoryVM Category { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public StockEntity Stock { get; set; }
        public bool Enable { get; set; }
    }


    public class ProductSearchVM
    {
        public CategoryVM Category { get; set; }
        public string Name { get; set; }
        public int HeightPrice { get; set; }
        public int LowPrice { get; set; }

        public GenderCategoryVM Gender { get; set; }
    }

    public static class ProductVMExtenssion
    {
        public static ProductVM ToVM(this ProductDTO dto)
        {
            return new ProductVM
            {
                Id = dto.Id,
                Category = dto.Category.ToVM(),
                Name = dto.Name,
                Price = dto.Price,
                Image = dto.Image,
                Description = dto.Description,
                Stock = dto.Stock,
                Enable = dto.Enable
            };
        }

       
    }


   
}