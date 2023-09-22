using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
		public int Qty { get; set; }
		public int Price { get; set; }
		public string Size { get; set; }
		public int SubTotal { get; set; }
	}

	public static class CartItemDTOExtenssion
	{
		public static CartItemDTO ToEntity(this CartItemEntity entity)
		{
			return new CartItemDTO
			{
				Id = entity.Id,
				Order = entity.Order.ToDTO(),
				Product = entity.Product.ToDTO(),
				ProductName = entity.ProductName,
				Qty = entity.Qty,
				Price = entity.Price,
				Size = entity.Size,
				SubTotal = entity.SubTotal,
			};
		}

		//public static CartItemDTO ToDTO(this CartItemVM vm)
		//{
		//	return new CartItemDTO
		//	{
		//		Id = vm.Id,
		//		Order = vm.Order.ToDTO(),
		//		Product = vm.Product.ToDTO(),
		//		ProductName = vm.ProductName,
		//		Qty = vm.Qty,
		//		Price = vm.Price,
		//		Size = vm.Size,
		//		SubTotal = vm.SubTotal,
		//	};
		//}
	}
}