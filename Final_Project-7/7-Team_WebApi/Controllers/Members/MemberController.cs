using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _7_Team_WebApi.Controllers.Members
{
    
    public class MemberController : Controller
    {
        MemberService member = new MemberService();

        // GET: Member

        [UserAuthorize(Functions = "2")]
        [HttpGet]
        public ActionResult Index()
        {
            var memberVM = member.GetAll().Select(x => x.ToVM());   

            return View(memberVM);
        }

        [UserAuthorize(Functions = "2")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var memberVM = member.Get(id).ToVM();

            return View(memberVM);
        }

        [UserAuthorize(Functions = "2")]
        [HttpPost]
        public ActionResult Edit(MemberVM memberVM)
        {
            if (ModelState.IsValid)
            {
                member.Update(memberVM.ToDTO());

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}