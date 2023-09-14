using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}