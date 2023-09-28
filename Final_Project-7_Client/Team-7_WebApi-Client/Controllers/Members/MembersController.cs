using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Infra;
using Team_7_WebApi_Client.Models.Views;
using Team_7_WebApi_Client.Models.Views.Members;
using Newtonsoft.Json.Linq;

namespace Team_7_WebApi_Client.Controllers
{
    public class MembersController : Controller
    {
        // GET: Members
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <returns></returns>
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

        public ActionResult ValidateAccount(RegisterVm vm)
        {
            var db = new AppDbContext();
            var memberInDb = db.Members.FirstOrDefault(p => p.Account == vm.Account);

            bool result=memberInDb==null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
		public ActionResult Logout() 
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/Members/Login");
		}

        /// <summary>
        /// 修改用戶資料
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 修改密碼（知道原密碼）
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult EditPassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditPassword(EditPasswordVm vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            try
            {
                var currentAccount = User.Identity.Name;
                ChangePassword(vm, currentAccount);
            }catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        private void ChangePassword(EditPasswordVm vm, string currentAccount)
        {
            var db = new AppDbContext();
            var memberInDb = db.Members.FirstOrDefault(p => p.Account == currentAccount);

            if (memberInDb == null)
            {
                throw new Exception("帳號不存在");
            }
            var salt = HashUtility.GetSalt();

            //compare origin password
            var hashedOrignPassword = HashUtility.ToSHA256(salt, vm.OriginalPassword);
            if (string.Compare(memberInDb.Password.Trim(), hashedOrignPassword, true) != 0)
            {
                throw new Exception("原始密碼不正確");
            }

            //hash new password
            var hashedPassword = HashUtility.ToSHA256(salt, vm.Password);

            memberInDb.Password = hashedPassword;
            db.SaveChanges();
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
            if (member.Enable == false)
            {
                throw new Exception("該帳號已被停用");
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

        [Route("GetMembers")]
        [Route("{GetMembers}/{id}")]
        public ActionResult GetMembers(int? id)
        {
            var db = new AppDbContext();
            var obj = new JObject();


            if (!string.IsNullOrWhiteSpace(id.ToString()))
            {
                var memberInDb = db.Members.Where(x => x.Id == id).Select(x => new { x.Id, x.Account, x.Email, x.FirstName, x.LastName, x.Enable }).ToList();

                if (!memberInDb.Any())
                {
                    throw new Exception("帳號不存在");

                }
                obj.Add("members", JToken.FromObject(memberInDb));


            }
            else
            {
                var memberInDb = db.Members.Select(x => new { x.Id, x.Account, x.Email, x.FirstName, x.LastName, x.Enable }).ToList();
                var members_count = db.Members.Count();

                obj.Add("members", JToken.FromObject(memberInDb));
                obj.Add("members_count", members_count);
            }


            return Content(obj.ToString(), "application/json");

        }
    }
}