using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Views
{
	public class CartItemVM
	{
		public int Id { get; set; }
		public OrderVM Order { get; set; }
		public ProductVM Product { get; set; }
		public string ProductName { get; set; }
		public int Qty { get; set; }
		public int Price { get; set; }
		public string Size { get; set; }
		public int SubTotal { get; set; }
	}

	public static class CartItemVMExtenssion
	{
		public static CartItemVM ToVM(this CartItemDTO dto)
		{
			return new CartItemVM
			{
				Id = dto.Id,
				Order = dto.Order.ToVM(),
				Product = dto.Product.ToVM(),
				ProductName = dto.ProductName,
				Qty = dto.Qty,
				Price = dto.Price,
				Size = dto.Size,
				SubTotal = dto.SubTotal,
			};
		}
	}
}