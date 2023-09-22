using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Repositories;

namespace Team_7_WebApi_Client.Services
{
	public class CartService
	{
		CartRepository repo = new CartRepository();

		/// <summary>
		/// Check if a cart exist by MemberId
		/// </summary>
		/// <param name="MemberId"></param>
		/// <returns></returns>
		public CartEntity IsCartExist(int MemberId)
		{
            return repo.Search(MemberId);
        }


		/// <summary>
		/// Check if a cart item exist by Size and ProductId , return the CartItemId or null
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public CartItemDTO IsCartItemExist(CartDTO dto , int ProductId , string Size)
		{
			var Cartitems = dto.CartItems;

			var CartItem = Cartitems.Select(c => c).Where(c => c.Id == ProductId && c.Size == Size).FirstOrDefault();

			return CartItem;
		}
	}
}