﻿using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _7_Team_WebApi.Controllers.Categories
{
    public class CategoryController : Controller
    {
        // GET: Category
        [UserAuthorize(Functions = "2")]
        public ActionResult CategoryMaintain()
        {
            return View();
        }
    }
}