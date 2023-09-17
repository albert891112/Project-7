using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Models.DTOS
{
    public class StockDTO
    {
        public int Id { get; set; }
        public int S { get; set; }
        public int M { get; set; }
        public int L { get; set; }
        public int XL { get; set; }
    }
    
    public static class StockDTOExtenssion
    {
        public static StockDTO ToDTO(this StockEntity entity)
        {
            return new StockDTO
            {
                Id = entity.Id,
                S = entity.S,
                M = entity.M,
                L = entity.L,
                XL = entity.XL
            };
        }
    }
}