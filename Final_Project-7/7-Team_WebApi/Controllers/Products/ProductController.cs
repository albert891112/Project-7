using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _7_Team_WebApi.Controllers.Products
{
    public class ProductController : Controller
    {
        // GET: Product

        [UserAuthorize(Functions = "1")]
        public ActionResult ProductMaintain()
        {
            return View();
        }
    }
}