using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.DTOs
{
    public class StockDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int S { get; set; }
        public int M { get; set; }
        public int L { get; set; }
        public int XL { get; set; }
    }

    public class StockUploadDTO
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public int? S { get; set; }
        public int? M { get; set; }
        public int? L { get; set; }
        public int? XL { get; set; }
    }

    public static class StockDTOExtenssion
    {
        public static StockDTO ToDTO(this StockEntity entity)
        {
            return new StockDTO
            {
                Id = (int)entity.Id,
                ProductId = (int)entity.ProductId,
                S = (int)entity.S,
                M = (int)entity.M,
                L = (int)entity.L,
                XL = (int)entity.XL
            };
        }

        public static StockDTO ToEntity(this StockVM vm)
        {
            return new StockDTO
            {
                Id = vm.Id,
                ProductId = vm.ProductId,
                S = vm.S,
                M = vm.M,
                L = vm.L,
                XL = vm.XL
            };
        }
    }
}