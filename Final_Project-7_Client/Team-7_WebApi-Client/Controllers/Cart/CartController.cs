using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Controllers.Cart
{
    public class CartController : Controller
    {
		// GET: Cart
		[Authorize]
		public ActionResult Cart()
		{
			string buyer = User.Identity.Name;
			CartVM cart = GetOrCreateCart(buyer);

			return View(cart);
		}


		private CartVM GetOrCreateCart(string buyer)
		{
			var db = new AppDbContext();

			var cart = db.Carts.FirstOrDefault(x => x.Member.Account == buyer);

			if (cart == null)//立刻新增一筆傳回
			{
				cart = new Team_7_WebApi_Client.Models.EFModels.Cart
				{
					Member = new Member
					{
						Account = buyer,
					}
				};
				db.Carts.Add(cart);
				db.SaveChanges();

				return new CartVM
				{
					Id = cart.Id,
					Member = new MemberVM
					{
						Account = buyer,
					},
					CartItems = new List<CartItemVM>()
				};
			}


			//傳回目前購物車主檔/明細檔紀錄
			return new CartVM
			{
				Id = cart.Id,
				Member = new MemberVM { Account = buyer },
				CartItems = cart.CartItems.Select(x => new CartItemVM
				{
					Id = x.Id,
					Product = new ProductVM
					{
						Id = x.Product.Id,
						Name = x.Product.Name,
						Price = x.Product.Price,
					},
					Qty = x.Qty,
				})

			};
		}
	}
}
