using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public CategoryEntity Category{ get; set; }
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
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int GenderId { get; set; }
        public string Name { get; set; }
    }
}