using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Infra;
using Team_7_WebApi_Client.Models.Views;
using Team_7_WebApi_Client.Models.Views.Members;


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
				RegisterMember(vm);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);
			}
			return View("RegisterConfirm");
		}

		//Login View
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginVm vm)
		{
			if (!ModelState.IsValid) { return View(vm); }
			try
			{
				ValidLogin(vm);
			}catch(Exception ex) 
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);
			}

			var processResult = ProcessLogin(vm);

			Response.Cookies.Add(processResult.Cookie);

			return Redirect(processResult.ReturnUrl);
		}

		private (string ReturnUrl,HttpCookie Cookie) ProcessLogin(LoginVm vm)
		{
			throw new NotImplementedException();
		}

		private void ValidLogin(LoginVm vm)
		{
			var db = new AppDbContext();

			//check account
			var member=db.Members.FirstOrDefault(p=>p.Account==vm.Account);
			if(member == null)
			{
				throw new Exception("帳號或密碼錯誤");
			}

			//Hash then compare
			var salt = HashUtility.GetSalt();
			var hashedPassword = HashUtility.ToSHA256(vm.Password, salt);

			if(string.Compare(member.Password, hashedPassword, true) != 0)
			{
				throw new Exception("帳號或密碼錯誤");
			}
		}

		public ActionResult Logout() { return View(); }

		private void RegisterMember(RegisterVm vm)
		{
			var db = new AppDbContext();

			var memberInDb=db.Members.FirstOrDefault(p=>p.Account==vm.Account);
			if (memberInDb!=null)
			{
				throw new Exception("該帳號已有人使用");
			}

			//vm to Member
			var member = vm.ToEFModel();

			//EF存進資料庫
			db.Members.Add(member);
			db.SaveChanges();
		}
	}
}