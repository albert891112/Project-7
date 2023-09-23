using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class CartDTO
	{
		public int Id { get; set; }
	    public int MemberId{ get; set; }

		public List<CartItemDTO> CartItems { get; set; }
	}

	public class CartCreateDTO
	{
        public int Id { get; set; }
        public int MemberId { get; set; }
        public CartItemCreateDTO CartItem { get; set; }

    }

	public static class CartDTOExtenssion
	{
		public static CartCreateDTO ToDTO(this CartCreateVM vm)
		{
			

			return new CartCreateDTO
			{
				Id = vm.Id,
				MemberId = vm.MemberId,
				CartItem = vm.CartItem.ToDTO(),

			};

			
		}

		public static CartDTO ToDTO(this CartEntity entity)
		{
            return new CartDTO
			{
                Id = entity.Id,
                MemberId = entity.MemberId,
                CartItems = entity.CartItems.Select(x => x.ToDTO()).ToList(),
            };
        }
	}



	
}