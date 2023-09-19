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
        public GenderCategoryVM Gender { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int S { get; set; }
        public int M { get; set; }
        public int L { get; set; }
        public int XL { get; set; }
        public bool Enable { get; set; }
    }


    public class ProductSearchVM
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string HightPrice { get; set; }
        public string LowPrice { get; set; }

        public string Gender { get; set; }
    }

    public static class ProductVMExtenssion
    {
        public static ProductVM ToVM(this ProductDTO dto)
        {
            return new ProductVM
            {
                Id = dto.Id,
                Category = dto.Category.ToVM(),
                Gender = dto.Gender.ToVM(),
                Name = dto.Name,
                Price = dto.Price,
                Image = dto.Image,
                Description = dto.Description,
                S = dto.Stock.S,
                M = dto.Stock.M,
                L = dto.Stock.L,
                XL = dto.Stock.XL,
                Enable = dto.Enable
            };
        }

       
    }


   
}