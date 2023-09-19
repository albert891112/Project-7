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

		public ActionResult Logout() 
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/Members/Login");
		}

        [Authorize]
        public ActionResult EditProfile()
        {
            var currentUserAccount = User.Identity.Name;
            var vm = GetMemberProfile(currentUserAccount);

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(EditProfileVm vm)
        {
            var currentUserAccount=User.Identity.Name;
            if (!ModelState.IsValid) { return View(vm); }
            try
            {
                UpdateProfile(vm, currentUserAccount);
            }catch(Exception ex)
            {
                ModelState.AddModelError("",ex.Message); return View(vm);
            }
            return RedirectToAction("Index"); 
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

        private (string ReturnUrl, HttpCookie Cookie) ProcessLogin(LoginVm vm)
        {
            var rememberMe = vm.RememberMe;
            var account = vm.Account;
            var roles = string.Empty;

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
            var value = FormsAuthentication.Encrypt(ticket);

            //add in cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);

            //get return url
            var url = FormsAuthentication.GetRedirectUrl(account, true);

            return (url, cookie);
        }

        private void ValidLogin(LoginVm vm)
        {
            var db = new AppDbContext();

            //check account
            var member = db.Members.FirstOrDefault(p => p.Account == vm.Account);
            if (member == null)
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

        private object GetMemberProfile(string currentUserAccount)
        {
            //get members
            var db = new AppDbContext();

            var member = db.Members.FirstOrDefault(m => m.Account == currentUserAccount);
            if(member == null)
            {
                throw new Exception("帳號不存在");
            }

            var vm = member.ToEditProfileVm();

            return vm;
        }
        private void UpdateProfile(EditProfileVm vm, string currentUserAccount)
        {
            var db=new AppDbContext();
            var member = db.Members.FirstOrDefault(p => p.Id == vm.Id);

            if(member.Account!=currentUserAccount)
            {
                throw new Exception("您沒有權限修改別人的資料");
            }

            member.FirstName = vm.FirstName;
            member.LastName = vm.LastName;
            member.Email = vm.Email;

            db.SaveChanges();
        }

        //private bool CheckAccountExists(RegisterVm vm)
        //{
        //	using(var db = new AppDbContext())
        //	{
        //		//check account in members
        //		return db.Members.Any(p=>p.Account==vm.Account);
        //	}
        //      }

        //[HttpPost]
        //public JsonResult CheckAccountAvailability(RegisterVm vm)
        //{
        //	bool isAccountExists=CheckAccountExists(vm);

        //	return Json(new {available=!isAccountExists});
        //}
    }
}