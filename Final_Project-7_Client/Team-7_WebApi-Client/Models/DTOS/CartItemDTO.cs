using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.UI;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class CartItemDTO
	{
		public int Id { get; set; }
		public OrderDTO Order { get; set; }
		public ProductDTO Product { get; set; }
		public string ProductName { get; set; }
		public int ProductId { get; set; }	
		public int Qty { get; set; }
		public int Price { get; set; }
		public string Size { get; set; }
		public int SubTotal { get; set; }
	}

	public class CartItemCreateDTO
	{
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public string Size { get; set; }
        public int CartId { get; set; }
    }

	public static class CartItemDTOExtenssion
	{
		public static CartItemCreateDTO ToDTO(this CartItemCreateVM vm)
		{
            return new CartItemCreateDTO
			{
                Id = vm.Id,
                ProductId = vm.ProductId, 
				CartId = vm.CartId,
                Qty = vm.Qty,
                Size = vm.Size,
            };
        }


		public static CartItemDTO ToDTO(this CartItemEntity entity)
		{
            return new CartItemDTO
			{
                Id = entity.Id,
                ProductId = entity.ProductId,
                Qty = entity.Qty,
                Size = entity.Size,
                Product = entity.Product.ToDTO(),
            };
        }
	}
}