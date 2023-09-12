using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class StockEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int S { get; set; }
        public int M { get; set; }
        public int L { get; set; }
        public int XL { get; set; }
    }
}