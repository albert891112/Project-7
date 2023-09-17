using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class CartDTO
	{
		public int Id { get; set; }
		//todo public MemberEntity Member { get; set; }
	}


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


	public static class CartDTOExtenssion
	{
		public static CartDTO ToDTO(this CartEntity entity)
		{
			return new CartDTO
			{
				Id = entity.Id,
				//todo Member = entity.MemberEntity.Id,

			};
		}		
	}


	public static class CartItemDTOExtenssion
	{
		public static CartItemDTO ToDTO(this CartItemEntity entity)
		{
			return new CartItemDTO
			{
				Id = entity.Id,
				Order =entity.Order.ToDTO(),
				Product = entity.Product.ToDTO(),
				ProductName = entity.ProductName,
				Qty = entity.Qty,
				Price = entity.Price,
				Size = entity.Size,
				SubTotal = entity.SubTotal,
			};
		}
	}
}