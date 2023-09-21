using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team_7_WebApi_Client.Controllers.Cart
{
    public class CartsController : Controller
    {
        // GET: Carts
        public ActionResult Cart()
        {
            return View();
        }
    }
}