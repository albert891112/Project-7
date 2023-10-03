using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _7_Team_WebApi.Controllers
{
    public class HomeController : Controller
    {

        [UserAuthorize]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
