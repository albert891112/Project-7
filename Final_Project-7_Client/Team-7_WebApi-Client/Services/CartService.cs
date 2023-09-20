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
		public List<CartDTO> GetAll()
		{
			List<CartEntity> carts = repo.GetAll();

			List<CartDTO> cartDTOs = carts.Select(c => c.ToDTO()).ToList();

			return cartDTOs;
		}
	}
}