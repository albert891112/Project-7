﻿using System;
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Account"></param>
		/// <returns></returns>
		public CartDTO ShowCart(string Account)
		{
			CartEntity cart = this.repo.GetCartByMember(Account);

			List<CartItemEntity> cartitem = this.repo.GetCartItem(cart);

			if (cartitem.Count > 0)
			{
				cart.CartItems = cartitem;
			}

			return cart.ToDTO();
		}

		/// <summary>
		/// 如果Cart不存在,創建一個Cart
		/// </summary>
		public void CreateCart()
		{
			//get MemberId by Account
			string account = HttpContext.Current.User.Identity.Name;
			int MemberId = memberRepo.GetIdByAccount(account);

			//Get Cart by MemberId
			var existCart = IsCartExist(account);

			if (existCart == null)
			{
				//Create new cart
				CartCreateEntity newCart = new CartCreateEntity
				{
					MemberId = MemberId,
				};

				this.repo.Create(newCart);
			}
		}



		/// <summary>
		/// 利用account取得memberId,如果Cart不存在,創建一個Cart與CartItem,如果Cart存在,則更新CartItem
		/// </summary>
		/// <param name="cart"></param>
		public void AddCartItem(CartItemCreateDTO cart)
		{
			//get MemberId by Account
			string account = HttpContext.Current.User.Identity.Name;
			int MemberId = memberRepo.GetIdByAccount(account);

			//Get Cart by MemberId
			var existCart = IsCartExist(account);
			try 
			{
                if (existCart == null)
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
			catch (Exception ex)
			{
				throw ex;
			}

			
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="MemberId"></param>
		/// <returns></returns>
		public CartEntity IsCartExist(string Account)
		{

			return this.repo.GetCartByMember(Account);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<ShippingDTO> GetShipping()
		{
			List<ShippingEntity> entities = this.repo.GetShippings();

			List<ShippingDTO> dtos = entities.Select(s => s.ToDTO()).ToList();

			return dtos;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<PaymentDTO> GetPayment()
		{
			List<PaymentEntity> entities = this.repo.GetPayments();

			List<PaymentDTO> dtos = entities.Select(p => p.ToDTO()).ToList();

			return dtos;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
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

	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="CartItemId"></param>
		public void DeleteCartItem(int CartItemId)
		{
            this.repo.DeleteCartItem(CartItemId);
        }
	
	}

}