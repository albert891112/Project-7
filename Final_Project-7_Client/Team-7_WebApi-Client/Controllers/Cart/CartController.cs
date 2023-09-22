using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Controllers.Cart
{
    public class CartController : Controller
    {
		// GET: Cart
		[Authorize]
		public ActionResult AddItem(int productId)
		{
			string buyer= User.Identity.Name;
            int memberId = GetMemberIdByAccount(buyer);
			AddToCart(memberId, productId,1);

			return new EmptyResult();
		}

        [Authorize]
        public ActionResult Info()
        {
            string buyer = User.Identity.Name;
            CartVM cart = GetOrCreateCart(GetMemberIdByAccount(buyer));

            return View(cart);
        }

        [Authorize]
        public ActionResult UpdateItem(int productId,int newQty)
        {
            var buyer = User.Identity.Name;
            newQty=newQty<0?0:newQty;

            UpdateItemQty(GetMemberIdByAccount(buyer), productId, newQty);

            return new EmptyResult();
        }

        private void UpdateItemQty(int memberId, int productId, int newQty)
        {
            var cart=GetOrCreateCart(memberId);
            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);
            if (cartItem == null) return;

            var db= new AppDbContext();
            if(newQty == 0)
            {
                var item=db.CartItems.Find(cartItem.Id);
                db.CartItems.Remove(item);
                db.SaveChanges();
                return;
            }
            else
            {
                var cartItemInDb=db.CartItems.FirstOrDefault(x => x.CartId == cart.Id);
                cartItem.Qty = newQty;
                db.SaveChanges();
            }
            
        }

        private int GetMemberIdByAccount(string account)
        {
            var db = new AppDbContext();
            var member = db.Members.FirstOrDefault(x => x.Account == account);

            if(member == null)
            {
                throw new Exception("Member not found");
            }

            return member.Id;
        }

        private void AddToCart(int memberId, int productId, int qty)
        {
            CartVM cart = GetOrCreateCart(memberId); //get cart of buyer, if not exist, create new cart

			AddCartItem(cart, productId, qty); //add product to cart
        }

        private void AddCartItem(CartVM cart, int productId, int qty)
        {
            var db = new AppDbContext(); 

            var cartItem = db.CartItems.FirstOrDefault(x => x.CartId == cart.Id && x.ProductId == productId); //check if product exist in cart
            if(cartItem==null)
            {
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Qty = qty
                };

                db.CartItems.Add(cartItem);
                db.SaveChanges();
                return;
            }
            else
            {
                cartItem.Qty += qty;
                db.SaveChanges();
            }
        }

        private CartVM GetOrCreateCart(int memberId)
        {
            var db = new AppDbContext();

            var cart = db.Carts.FirstOrDefault(x => x.MemberId== memberId);
            if (cart == null)
            {
                cart = new Models.EFModels.Cart
                {
                    MemberId = memberId
                };

                db.Carts.Add(cart);
                db.SaveChanges();

                return new CartVM
                {
                    Id = cart.Id,
                    MemberId = cart.MemberId,
                    CartItems = new List<CartItemVM>()
                };
            }
            //return CartItems

            return new CartVM
            {
                Id = cart.Id,
                MemberId = cart.MemberId,
                CartItems = cart.CartItems.Select(x => new CartItemVM
                {
                    Id = x.Id,
                    CartId = x.CartId,
                    ProductId = x.ProductId,
                    Qty = x.Qty,
                    Size = x.Size,
                    Product = new ProductVM
                    {
                        Id = x.Product.Id,
                        Name = x.Product.Name,
                        Price = x.Product.Price,
                    }
                })
            };
        }
    }
}
