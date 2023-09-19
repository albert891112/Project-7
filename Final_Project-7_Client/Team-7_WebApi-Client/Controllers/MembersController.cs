using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
			var rememberMe=vm.RememberMe;
			var account=vm.Account;
			var roles=string.Empty;

			//ticket
			var ticket =
				new FormsAuthenticationTicket(
					1,
					account,
					DateTime.Now,
					DateTime.Now.AddDays(2),
					rememberMe,
					roles,
					"/"
					);

			//Encrypt
			var value=FormsAuthentication.Encrypt(ticket);

			//add in cookie
			var cookie=new HttpCookie(FormsAuthentication.FormsCookieName,value);

			//get return url
			var url = FormsAuthentication.GetRedirectUrl(account, true);

			return (url, cookie);
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
			var hashedPassword = HashUtility.ToSHA256(salt, vm.Password);
			// var pwd=member.Password;
			// var result = string.Compare(member.Password.Trim(), hashedPassword, true);

            if (string.Compare(member.Password.Trim(), hashedPassword, true) != 0)
			{
				throw new Exception("帳號或密碼錯誤");
			}
		}

		public ActionResult Logout() 
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/Members/Login");
		}

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