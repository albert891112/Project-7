using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class StockEntity
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public int? S { get; set; }
        public int? M { get; set; }
        public int? L { get; set; }
        public int? XL { get; set; }
    }

    public static class StockEntityExtenssion
    {
        public static StockEntity ToEntity(this StockDTO dto)
        {
            return new StockEntity
            {
                Id = dto.Id,
                ProductId = dto.ProductId,
                S = dto.S,
                M = dto.M,
                L = dto.L,
                XL = dto.XL
            };
        }
    }
 }