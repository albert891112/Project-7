using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _7_Team_WebApi.Controllers.PermissionControll
{
    public class PermissionControllController : Controller
    {
        UserServie serv = new UserServie();
        // GET: PermissionControll

        [UserAuthorize(Functions = "5")]
        public ActionResult ToPermissionControll()
        {
            return View();
        }

        [UserAuthorize(Functions = "6")]
        public ActionResult ToEdit()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult InsufficientPermissions()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserVM formData)
        {
          
       
            if (!this.serv.IsValid(formData.Account.Trim(), formData.Password.Trim()))
            {
      
                ModelState.AddModelError(string.Empty, "帳號或密碼錯誤");
                return View(formData);
            }

            HttpCookie cookie;
            var returnUrl = this.serv.ProcessLogin(formData.Account, false, out cookie);


            //add Cookie to Cookiecollection
            Response.Cookies.Add(cookie);

            return Redirect(returnUrl);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        
    }
}