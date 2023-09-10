using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _7_Team_WebApi.Controllers
{
    public class CategoryController : Controller
    {

        CategoryService serv= new CategoryService();


        // GET: Category
        public ActionResult Index()
        {
            var categoryVM = serv.GetAll().Select(x => x.ToVM());

            return View(categoryVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                serv.Create(categoryVM.ToDTO());
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var categoryVM = serv.Get(id).ToVM();

            return View(categoryVM);
        }

        [HttpPost]
        public ActionResult Edit(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                serv.Update(categoryVM.ToDTO());

                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var categoryVM = serv.Get(id).ToVM();

            if(categoryVM == null)
            {
                return HttpNotFound();
            }

            return View(categoryVM);
        }

        [HttpPost]
        public ActionResult Delete(CategoryVM categoryVM)
        {
            serv.Delete(categoryVM.Id);

            return RedirectToAction("Index");
        }
    }
}