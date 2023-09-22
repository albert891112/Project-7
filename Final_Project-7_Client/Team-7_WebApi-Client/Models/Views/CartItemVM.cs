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
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public ProductVM Product { get; set; }
		public int Qty { get; set; }
        public string Size { get; set; }
        public int SubTotal=>Product.Price*Qty;
	}

	public static class CartItemVMExtenssion
	{
		public static CartItemVM ToVM(this CartItemDTO dto)
		{
			return new CartItemVM
			{
				Id = dto.Id,
				Product = dto.Product.ToVM(),
				Qty = dto.Qty,
			};
		}
	}
}