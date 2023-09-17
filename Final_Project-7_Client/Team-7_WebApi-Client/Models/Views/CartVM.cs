using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Models.Views
{
	public class CartVM
	{
		public int Id { get; set; }
		//todo public MemberEntity Member { get; set; }
	}


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



	public static class CartVMExtenssion
	{
		public static CartVM ToVM(this CartDTO dto)
		{
			return new CartVM
			{
				Id = dto.Id,
				//todo Member = dto.Member.ToVM(),
			};
		}
	}

	public static class CartItemVMExtenssion
	{
		public static CartItemVM ToDTO(this CartItemDTO dto)
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