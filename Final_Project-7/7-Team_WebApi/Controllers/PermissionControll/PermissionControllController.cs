using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _7_Team_WebApi.Controllers.PermissionControll
{
    public class PermissionControllController : Controller
    {
        // GET: PermissionControll
        public ActionResult ToPermissionControll()
        {
            return View();
        }

        public ActionResult ToEdit()
        {
            return View();
        }
    }
}