using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team_7_WebApi_Client.Services;

namespace Team_7_WebApi_Client.Controllers.Cart
{
    public class CartController : Controller
    {

        CartService serv = new CartService();
        // GET: Cart
        [Authorize]
        public ActionResult ToCart()
        {
          return View();
    
        }

        [Authorize]
        public ActionResult Checkout()
        {
			return View();
		}
    }
}