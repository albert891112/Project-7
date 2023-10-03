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
        public int MemberId { get; set; }

        public IEnumerable<CartItemVM> CartItems { get; set; }

        public int Total { get; set; }
    }

	public class CartCreateVM
	{
        public int Id { get; set; }
        public int MemberId { get; set; }
        public CartItemCreateVM CartItem { get; set; }

    }


    public static class CartVMExtenssion
    {
        public static CartVM ToVM(this CartDTO dto)
        {
            List<CartItemVM> cartItem = null;
            int total = 0;  
            if(dto.CartItems != null)
            {
                cartItem = dto.CartItems.Select(x => x.ToVM()).ToList();
                total = cartItem.Sum(x => x.SubTotal);
            }

            return new CartVM
            {
                Id = dto.Id,
                MemberId = dto.MemberId,
                CartItems = cartItem,
                Total = total
            };
        }
    } 


}