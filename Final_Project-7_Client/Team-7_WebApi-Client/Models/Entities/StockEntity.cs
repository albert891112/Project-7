using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;

namespace Team_7_WebApi_Client.Models.Entities
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

    public static class StockEntityExtenssion
    {
        public static StockEntity ToEntity(this Stock model)
        {
            return new StockEntity
            {
                Id = model.Id,
                S = model.S,
                M = model.M,
                L = model.L,
                XL = model.XL
            };
        }
    }   
}