using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Controllers
{
    public class MembersController : Controller
    {
        // GET: Members
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterVm vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}
			try
			{
				//RegisterMember(vm);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);
			}
			return View("RegisterConfirm");
		}
	}
}