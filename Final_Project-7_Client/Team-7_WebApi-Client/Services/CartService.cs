using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Repositories;

namespace Team_7_WebApi_Client.Services
{
	public class CartService
	{
		CartRepository repo = new CartRepository();
		MemberRepository memberRepo = new MemberRepository();


		public CartDTO ShowCart(string Account)
		{
			CartEntity cart = this.repo.GetCartByMember(Account);	

			return cart.ToDTO();
		}

		/// <summary>
		/// If Cart is exist, update cartItem or Create new cart
		/// </summary>
		/// <param name="cart"></param>
		public void AddCartItem(CartItemCreateDTO cart)
		{
			//get MemberId by Account
			string account = HttpContext.Current.User.Identity.Name;
			int MemberId = memberRepo.GetIdByAccount(account);

			//Get Cart by MemberId
			var existCart = IsCartExist(MemberId);

			if(existCart == null)
			{
				//Create new cart
				CartCreateEntity newCart = new CartCreateEntity
				{
					MemberId = MemberId,
					CartItem = cart.ToEntity()
				};
				

				this.repo.Create(newCart);
			}
			else
			{
				cart.CartId = existCart.Id;

				CartItemCreateEntity cartItem = cart.ToEntity();

				this.repo.Upsert(cartItem);
			}
        }


		/// <summary>
		/// Check if a cart exist by MemberId
		/// </summary>
		/// <param name="MemberId"></param>
		/// <returns></returns>
		public Cart IsCartExist(int MemberId)
		{

            return  repo.Search(MemberId);

        }


		public List<ShippingDTO> GetShipping()
		{
			List<ShippingEntity> entities = this.repo.GetShippings();

			List<ShippingDTO> dtos = entities.Select(s => s.ToDTO()).ToList();

			return dtos;
		}

		public List<PaymentDTO> GetPayment()
		{
			List<PaymentEntity> entities = this.repo.GetPayments();

			List<PaymentDTO> dtos = entities.Select(p => p.ToDTO()).ToList();

			return dtos;
		}

		public List<GetCouponDTO> GetCoupon()
		{
			List<GetCouponEntity> entities = this.repo.GetCoupons();

			List<GetCouponDTO> dtos = entities.Select(c => c.ToDTO()).ToList();

			//如果Enabled = false , 則移除
			dtos.RemoveAll(c => c.Enabled == false);

			//如果截止日期小於今天，則移除
			dtos.RemoveAll(c => c.EndDate < DateTime.Now);


			return dtos;
		}

	}
}