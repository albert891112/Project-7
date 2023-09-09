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
    }
}